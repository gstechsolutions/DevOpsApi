namespace DevOpsApi.core.api.Models.Auth
{
    public class JwtTokenByUser
    {
        public string UserName { get; set; }

        public LoginTokenResponseModel LoginTokenModel { get; set; }
    }
}
