using DevOpsApi.core.api.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOpsApi.core.api.Models.Auth
{
    public class RolePolicyModel : BaseModel
    {                
        //public int? RoleId { get; set; } = default(int?);
        
        //public int? PolicyId { get; set; } = default(int?);

        public Role? Role { get; set; }

        public Policy? Policy { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}
