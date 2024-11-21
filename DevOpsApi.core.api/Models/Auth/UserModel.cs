using DevOpsApi.core.api.Data.Entities;

namespace DevOpsApi.core.api.Models.Auth
{
    public class UserModel : BaseModel
    {
        public int Id { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public int RoleId { get; set; }

        public Role? Role { get; set; }
    }
}
