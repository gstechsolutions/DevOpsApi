using AutoMapper;
using DevOpsApi.core.api.ConfigurationModel;
using DevOpsApi.core.api.Data;
using DevOpsApi.core.api.Data.Entities;
using DevOpsApi.core.api.FluentValidation;
using DevOpsApi.core.api.Models;
using DevOpsApi.core.api.Models.Auth;
using DevOpsApi.core.api.Models.POSTempus;
using DevOpsApi.core.api.Services.POSTempus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NodaTime;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DevOpsApi.core.api.Services.Auth
{
    public class JwtService : IJwtService
    {
        private readonly ILogger<JwtService> logger;
        private readonly IMapper mapper;
        private readonly IOptions<ServiceCoreSettings> settings;
        private readonly IConfiguration configuration;
        private readonly STRDMSContext context;
        private readonly IMemoryCache cache;
        private readonly IClock clock;
        private LoginTokenResponseModel TokenModel;        
        

        public JwtService(ILogger<JwtService> logger,
            IMapper mapper,
            IOptions<ServiceCoreSettings> settings,
            IConfiguration configuration,
            STRDMSContext context,
            IMemoryCache cache,
            IClock clock)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.settings = settings;
            this.configuration = configuration;
            this.context = context;
            this.cache = cache;
            this.clock = clock;
        }

        public async Task<LoginTokenResponseModel> EnsureTokenIsValid(LoginModel login)
        {
            var cacheKey = "JwtTokenList";
            var tokenList = new List<JwtTokenByUser>();

            //get the cacheToken list to see if userName contains a valid token, if not the generate it
            if (cache.TryGetValue(cacheKey, out tokenList))
            {
                var jwtTokenByUser = tokenList.Where(t => t.UserName.ToLower().Equals(login.UserName.ToLower())).FirstOrDefault();
                if (jwtTokenByUser != null)
                {
                    TokenModel = jwtTokenByUser.LoginTokenModel;
                }
            }

            //if less than 60 sec then get a new token
            if (TokenModel == null || TokenModel.ExpiresIn < 60 || 
               (TokenModel.Roles != null && !login.Roles.OrderBy(r => r).SequenceEqual(TokenModel.Roles.OrderBy(r => r))) )
            {
                return await GenerateJwtToken(login);
            }

            return TokenModel;
        }

        public async Task<LoginTokenResponseModel> GenerateJwtToken(LoginModel login)
        {
            var functionName = "GenerateJwtToken";            
            var token = new JwtSecurityToken();
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                this.logger.LogInformation($"{clock.GetCurrentInstant().ToDateTimeUtc().ToLocalTime()}: Entering {functionName}.");

                var jwtSettings = configuration.GetSection("JwtConfig");
                var issuer = jwtSettings["Issuer"];
                var audience = jwtSettings["Audience"];
                var key = jwtSettings["Key"];
                var tokenValidityMins = int.Parse(jwtSettings["TokenValidityMins"]);
                var tokenExpiryTimeStamp = DateTime.UtcNow.Add(TimeSpan.FromMinutes(tokenValidityMins));

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));                
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, login.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
                    new Claim("userName", login.UserName)
                };

                //Add roles as individual claims
                login.Roles.ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Issuer = issuer,
                    //Expires = tokenExpiryTimeStamp,
                    Expires = DateTime.UtcNow.AddSeconds(30),
                    Audience = issuer,
                    SigningCredentials = credentials
                };

                var tokenCreated = tokenHandler.CreateToken(tokenDescriptor);

                TokenModel = new LoginTokenResponseModel
                {
                    AccessToken = tokenHandler.WriteToken(tokenCreated),
                    // ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds
                    ExpiresIn = (tokenDescriptor.Expires.HasValue) ? (int)tokenDescriptor.Expires.Value.Subtract(DateTime.UtcNow).TotalSeconds 
                                                                    : 0,
                    UserName = login.UserName,
                    Roles = login.Roles
                };

                //add it to the token cached list using key,value pairs 
                await SetTokenCachedList(login);
            }
            catch (Exception ex)
            {
                TokenModel.SetError(ex.Message);

                this.logger.LogError($"{functionName} EXCEPTION- {clock.GetCurrentInstant().ToDateTimeUtc().ToLocalTime()}: {ex.Message}");
                if (ex.InnerException != null)
                {
                    this.logger.LogError($"{clock.GetCurrentInstant().ToDateTimeUtc().ToLocalTime()}: {ex.InnerException.Message}");
                }
            }
            finally
            {
                this.logger.LogInformation($"{clock.GetCurrentInstant().ToDateTimeUtc().ToLocalTime()}: Exited {functionName}.");
            }

            return await Task.FromResult(TokenModel);
        }

        public async Task<List<RolePolicyModel>> GetPoliciesByRole(int roleId)
        {
            var functionName = "GetPoliciesByRole";
            var polisyList = new List<RolePolicyModel>();

            try
            {
                this.logger.LogInformation($"{clock.GetCurrentInstant().ToDateTimeUtc().ToLocalTime()}: Entering {functionName}.");

                var entityList = await context.RolePolicies
                    .Where(rp => rp.RoleId == roleId)
                    .Include(rp => rp.Role)
                    .Include(rp => rp.Policy)
                    .ToListAsync();

                if (entityList != null)
                {
                    polisyList = mapper.Map<List<RolePolicy>,List<RolePolicyModel>>(entityList);
                }
               
            }
            catch (Exception ex)
            {
                this.logger.LogError($"{functionName} EXCEPTION- {clock.GetCurrentInstant().ToDateTimeUtc().ToLocalTime()}: {ex.Message}");
                if (ex.InnerException != null)
                {
                    this.logger.LogError($"{clock.GetCurrentInstant().ToDateTimeUtc().ToLocalTime()}: {ex.InnerException.Message}");
                }
            }
            finally
            {
                this.logger.LogInformation($"{clock.GetCurrentInstant().ToDateTimeUtc().ToLocalTime()}: Exited {functionName}.");
            }

            return polisyList;
        }

        /// <summary>
        /// UserModel needs to have username, password, roleId
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<UserModel> InsertNewUserModel(UserModel user)
        {
            var functionName = "InsertNewUserModel";
            var userValidator = new UserValidator();
           
            try
            {
                this.logger.LogInformation($"{clock.GetCurrentInstant().ToDateTimeUtc().ToLocalTime()}: Entering {functionName}.");

                var userValidatorResult = userValidator.Validate(user);
                if (!userValidatorResult.IsValid)
                {
                    throw new ArgumentException(userValidatorResult.ToString());
                }

                string decryptedPassword = AESHelper.DecryptPassword(user.Password);

                //now that the password is decrypted, hash it so it can be inserted in the database
                string hashedPassword = PasswordHelper.HashPassword(decryptedPassword);

                user.Password = hashedPassword;

                var userEntity = mapper.Map<UserModel,User>(user);

                if (userEntity != null)
                {
                    //do the insert into the db
                    context.Add(userEntity);
                    context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                user.SetError(ex.Message);
                this.logger.LogError($"{functionName} EXCEPTION- {clock.GetCurrentInstant().ToDateTimeUtc().ToLocalTime()}: {ex.Message}");
                if (ex.InnerException != null)
                {
                    this.logger.LogError($"{clock.GetCurrentInstant().ToDateTimeUtc().ToLocalTime()}: {ex.InnerException.Message}");
                }
            }
            finally
            {
                this.logger.LogInformation($"{clock.GetCurrentInstant().ToDateTimeUtc().ToLocalTime()}: Exited {functionName}.");
            }

            return user;
        }       

        private async Task<bool> SetTokenCachedList(LoginModel login)
        {
            var functionName = "SetTokenCachedList";
            var cacheKey = "JwtTokenList";
            var tokenList = new List<JwtTokenByUser>();
            var success = false;

            try
            {
                this.logger.LogInformation($"{clock.GetCurrentInstant().ToDateTimeUtc().ToLocalTime()}: Entering {functionName}.");

                if (!cache.TryGetValue(cacheKey, out tokenList))
                {
                    tokenList = new List<JwtTokenByUser>();
                    var newToken = new JwtTokenByUser
                    {
                        UserName = login.UserName,
                        LoginTokenModel = TokenModel
                    };
                    tokenList.Add(newToken);

                    cache.Set(cacheKey, tokenList, TimeSpan.FromHours(1));
                    success = true;
                }
                else
                {
                    var updateTokenListIX = tokenList.FindIndex(t => t.UserName.ToLower() == login.UserName.ToLower());
                    if (updateTokenListIX != -1)
                    {
                        //token for userName has been found, replace it
                        tokenList[updateTokenListIX] = new JwtTokenByUser
                        {
                            UserName = login.UserName,
                            LoginTokenModel = TokenModel
                        };
                    }
                }
            }
            catch(Exception ex)
            {
                this.logger.LogError($"{functionName} EXCEPTION- {clock.GetCurrentInstant().ToDateTimeUtc().ToLocalTime()}: {ex.Message}");
                if (ex.InnerException != null)
                {
                    this.logger.LogError($"{clock.GetCurrentInstant().ToDateTimeUtc().ToLocalTime()}: {ex.InnerException.Message}");
                }
            }
            finally
            {
                this.logger.LogInformation($"{clock.GetCurrentInstant().ToDateTimeUtc().ToLocalTime()}: Exited {functionName}.");
            }

            return await Task.FromResult(success);
        }

       
    }
}
