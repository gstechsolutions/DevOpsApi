namespace DevOpsApi.core.api.Models.Auth
{
    public class UserLoginRequestModel : BaseModel
    {
        public string UserName { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}
