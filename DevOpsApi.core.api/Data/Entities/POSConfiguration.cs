using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DevOpsApi.core.api.Data.Entities
{
    [Table("tblPOSConfiguration")]
    public class POSConfiguration
    {
        [Key]
        public int POSConfigurationID { get; set; }

        public int RNID { get; set; }

        public string RNCert { get; set; }

        public long CompanyID { get; set; }

        public long CompanyDepartmentID { get; set; }

    }
}
