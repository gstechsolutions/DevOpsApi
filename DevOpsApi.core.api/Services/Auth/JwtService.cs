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
using Newtonsoft.Json;
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

        public async Task<bool> DeleteUser(string userName)
        {
            var functionName = "DeleteUser";
            var success = false;
            try
            {
                this.logger.LogInformation($"{clock.GetCurrentInstant().ToDateTimeUtc().ToLocalTime()}: Entering {functionName}.");

                var user = await context.Users.Where(u => u.UserName.ToLower().Equals(userName.ToLower())).FirstOrDefaultAsync();

                if (user != null)
                {
                    context.Users.Remove(user);
                    await context.SaveChangesAsync();
                    success = true;
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

            return await Task.FromResult(success);
        }

        public async Task<LoginTokenResponseModel> EnsureTokenIsValid(UserLoginResponseModel login)
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
               //(TokenModel.Roles != null && !login.Roles.OrderBy(r => r).SequenceEqual(TokenModel.Roles.OrderBy(r => r))) )
               (TokenModel.RoleId != login.RoleId))
            {
                return await GenerateJwtToken(login);
            }

            return await Task.FromResult(TokenModel);
        }

        public async Task<LoginTokenResponseModel> GenerateJwtToken(UserLoginResponseModel login)
        {
            var functionName = "GenerateJwtToken";            
            var token = new JwtSecurityToken();
            var tokenHandler = new JwtSecurityTokenHandler();
            var roleModelList = new List<RolePolicyModel>();

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

                var rolesList = await context.RolePolicies.
                    Where(rp => rp.RoleId == login.RoleId)
                    .Include(rp => rp.Policy)
                    .ToListAsync();

                if (rolesList != null)
                {
                    roleModelList = mapper.Map<List<RolePolicy>,List<RolePolicyModel>>(rolesList);
                    //Add roles as individual claims
                    roleModelList.ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role.Policy?.Name)));
                }

                

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
                    Roles = roleModelList
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
        /// the user's data will be encrypted from the client side.
        /// filters will contain the decrypted data
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        public async Task<UserModelToInsert> InsertNewUserModel(RequestFilters filters)
        {
            var functionName = "InsertNewUserModel";
            var userValidator = new UserValidator();
            var userModel = new UserModelToInsert();
            try
            {
                this.logger.LogInformation($"{clock.GetCurrentInstant().ToDateTimeUtc().ToLocalTime()}: Entering {functionName}.");

                

                string decryptedPassword = AESHelper.DecryptPassword(filters.Data);

                if (!string.IsNullOrEmpty(decryptedPassword))
                {
                    //this will contains userName, password, and roleId.  Encrypt the password so it can be inserted in db
                    userModel = JsonConvert.DeserializeObject<UserModelToInsert>(decryptedPassword);

                    if (userModel != null)
                    {
                        var userValidatorResult = userValidator.Validate(userModel);

                        if (!userValidatorResult.IsValid)
                        {
                            throw new ArgumentException(userValidatorResult.ToString());
                        }

                        string hashedPassword = PasswordHelper.HashPassword(userModel.Password);
                        userModel.Password = hashedPassword;
                        var userEntity = mapper.Map<UserModelToInsert, User>(userModel);

                        if (userEntity != null)
                        {
                            //do the insert into the db
                            context.Add(userEntity);
                            context.SaveChanges();
                        }
                    }
                }                
            }
            catch (Exception ex)
            {
                userModel.SetError(ex.Message);
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

            return await Task.FromResult(userModel);
        }

        public async Task<UserLoginResponseModel> UserLogin(RequestFilters filters)
        {
            var functionName = "UserLogin";
            var userValidator = new UserLoginValidator();
            var userRequestModel = new UserLoginRequestModel();
            var userResponseModel = new UserLoginResponseModel();
            try
            {
                this.logger.LogInformation($"{clock.GetCurrentInstant().ToDateTimeUtc().ToLocalTime()}: Entering {functionName}.");

                string decryptedPassword = AESHelper.DecryptPassword(filters.Data);

                if (!string.IsNullOrEmpty(decryptedPassword))
                {                    
                    userRequestModel = JsonConvert.DeserializeObject<UserLoginRequestModel>(decryptedPassword);

                    if (userRequestModel != null)
                    {
                        var userValidatorResult = userValidator.Validate(userRequestModel);

                        if (!userValidatorResult.IsValid)
                        {
                            throw new ArgumentException(userValidatorResult.ToString());
                        }
                        //login logic verifying the hased password saved in DB user table
                        var authUser = await context.Users
                            .Where(u => u.UserName.ToLower().Equals(userRequestModel.UserName.ToLower()))
                            .FirstOrDefaultAsync();
                        
                        if (authUser != null)
                        {
                            //varify the hashed password
                            var verifyResult = PasswordHelper.VerifyPassword(userRequestModel.Password, authUser.Password);

                            if (verifyResult)
                            {
                                userResponseModel = new UserLoginResponseModel
                                {
                                    UserName = userRequestModel.UserName,
                                    RoleId = authUser.RoleId,
                                    Authenticated = true
                                };
                            }
                        }                        
                    }
                }
            }
            catch (Exception ex)
            {
                userResponseModel.SetError(ex.Message);
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

            return userResponseModel;
        }

        private async Task<bool> SetTokenCachedList(UserLoginResponseModel login)
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
