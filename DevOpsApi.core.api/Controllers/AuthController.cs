using DevOpsApi.core.api.Models.Auth;
using DevOpsApi.core.api.Models.POSTempus;
using DevOpsApi.core.api.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DevOpsApi.core.api.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IJwtService jwtService;

        public AuthController(IConfiguration configuration, IJwtService jwtService)
        {
            this.configuration = configuration;
            this.jwtService = jwtService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/auth/login")]
        public IActionResult Login([FromBody] LoginModel login)
        {
            // Perform user validation here (e.g., check user credentials in the database)

            if (!string.IsNullOrEmpty(login.UserName))
            {
                //var token = GenerateJwtToken(login);
                //var token = jwtService.GenerateJwtToken(login);
                var token = jwtService.EnsureTokenIsValid(login);
                return Ok(new { token });
            }

            return Unauthorized();
        }

        [HttpGet]
        [Route("api/auth/policy/role/{id}")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<RolePolicyModel>), description: "Used to retrieve list of policies by roleId.")]
        public async Task<IActionResult> GetPoliciesByRole(int id)
        {
            var list = await jwtService.GetPoliciesByRole(id);
            return Ok(list);
        }

        
        [AllowAnonymous]
        [HttpPost]
        [Route("api/auth/user/insert")]
        public IActionResult InsertNewUserModel([FromBody] UserModel user)
        {
            var result = jwtService.InsertNewUserModel(user);
            return Ok(result);

            return Unauthorized();
        }

    }

}

