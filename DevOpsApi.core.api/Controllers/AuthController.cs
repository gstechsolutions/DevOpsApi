using DevOpsApi.core.api.Models;
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

        /// <summary>
        /// return Ok(new { token }); will include token : {} plus all the properties of 
        /// LoginTokenResponseModel
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("api/auth/login")]
        public async Task<IActionResult> Login([FromBody] RequestFilters filters)
        {
            // Perform user validation here (e.g., check user credentials in the database)

            var userAuth = await jwtService.UserLogin(filters);
            if (userAuth != null && userAuth.Authenticated)
            {

                var token = await jwtService.EnsureTokenIsValid(userAuth);                
                return Ok(token);
            }

            return Unauthorized();
        }

        [HttpGet]
        [Route("api/auth/policy/role/{id}")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<RolePolicyModel>), description: "Used to retrieve list of policies by roleId.")]
        public async Task<IActionResult> GetPoliciesByRole(int id)
        {
            //var list = await jwtService.GetPoliciesByRole(id);
            return Ok("{\"data\":\"hello world\" }");
        }

        
        [AllowAnonymous]
        [HttpPost]
        [Route("api/auth/user/insert")]
        public async Task<IActionResult> InsertNewUserModel([FromBody] RequestFilters filters)
        {
            var result = await jwtService.InsertNewUserModel(filters);
            return Ok(result);            
        }
        //public async Task<UserLoginResponseModel> UserLogin(RequestFilters filters)

        [HttpGet]
        [Route("api/auth/user/delete/{name}")]
        [SwaggerResponse(statusCode: 200, type: typeof(bool), description: "Used to delete user.")]
        public async Task<IActionResult> DeleteUser(string name)
        {
            var success = await jwtService.DeleteUser(name);
            return Ok(success);
        }

    }

}

