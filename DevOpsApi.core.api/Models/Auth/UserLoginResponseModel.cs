namespace DevOpsApi.core.api.Models.Auth
{
    public class UserLoginResponseModel : BaseModel
    {
        public string UserName { get; set; } = string.Empty;        

        public int RoleId { get; set; }

        public bool Authenticated { get; set; }
    }
}
