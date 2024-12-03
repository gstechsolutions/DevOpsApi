namespace DevOpsApi.core.api.Models.Auth
{
    public class LoginTokenResponseModel : BaseModel
    {
        public string AccessToken { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public int ExpiresIn { get; set; }

        public List<RolePolicyModel>? Roles { get; set; }

        public int RoleId { get; set; }
    }
}
