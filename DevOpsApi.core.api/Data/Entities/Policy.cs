using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOpsApi.core.api.Data.Entities
{
    [Table("Policy")]
    public class Policy
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        
        //public int RoleId { get; set; }

        //[ForeignKey("RoleId")]
        //public Role? Role { get; set; }
    }
}
