using DevOpsApi.core.api.Models;
using DevOpsApi.core.api.Models.Auth;
using System.IdentityModel.Tokens.Jwt;

namespace DevOpsApi.core.api.Services.Auth
{
    public interface IJwtService
    {
        Task<LoginTokenResponseModel> GenerateJwtToken(UserLoginResponseModel login);

        Task<LoginTokenResponseModel> EnsureTokenIsValid(UserLoginResponseModel login);

        Task<List<RolePolicyModel>> GetPoliciesByRole(int roleId);

        Task<UserModelToInsert> InsertNewUserModel(RequestFilters filters);

        Task<UserLoginResponseModel> UserLogin(RequestFilters filters);

        Task<bool> DeleteUser(string userName);
    }
}
