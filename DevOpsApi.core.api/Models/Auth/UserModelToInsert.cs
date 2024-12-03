namespace DevOpsApi.core.api.Models.Auth
{
    public class UserModelToInsert :BaseModel
    {
        public string UserName { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public int RoleId { get; set; }
    }
}
