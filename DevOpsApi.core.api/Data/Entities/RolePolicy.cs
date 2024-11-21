using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOpsApi.core.api.Data.Entities
{
    [Table("RolePolicies")]
    public class RolePolicy
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("RoleId")]
        public int? RoleId { get; set; } = default(int?);

        [ForeignKey("PolicyId")]
        public int? PolicyId { get; set;} = default(int?);

        public Role? Role { get; set; }

        public Policy? Policy { get; set; }

        public DateTime? CreatedAt { get; set; }



    }
}
