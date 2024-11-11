using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DevOpsApi.core.api.Data.Entities
{
    [Table("tblLocation")]
    public class Location
    {
        [Key]
        public long LocationID { get; set; }

        public required string LocationCode { get; set; }

        public string? LocationName { get; set; }

        public string? Address { get; set; }

        public bool? Active { get; set; }

        public string? RimproWarehouseCode { get; set; }

        public bool? IsPDC { get; set; }
    }
}
