using DevOpsApi.core.api.Models.Auth;
using System.IdentityModel.Tokens.Jwt;

namespace DevOpsApi.core.api.Services.Auth
{
    public interface IJwtService
    {
        Task<LoginTokenResponseModel> GenerateJwtToken(LoginModel login);

        Task<LoginTokenResponseModel> EnsureTokenIsValid(LoginModel login);

        Task<List<RolePolicyModel>> GetPoliciesByRole(int roleId);

        Task<UserModel> InsertNewUserModel(UserModel user);
    }
}
