namespace DevOpsApi.core.api.Models.Auth
{
    public class LoginModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public List<string> Roles { get; set; }
    }
}
