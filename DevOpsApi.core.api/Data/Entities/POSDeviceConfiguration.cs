using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DevOpsApi.core.api.Data.Entities
{
    [Table("tblPOSDeviceConfiguration")]
    public class POSDeviceConfiguration
    {
        [Key]
        public int POSDeviceConfigurationID { get; set; }

        public string Subscriberkey { get; set; }

        public string HostName { get; set; }

        public string DeviceAlias { get; set; }

        public int Active { get; set; }

        public long CompanyID { get; set; }

        public long CompanyDepartmentID { get; set; }

        public int POSConfigurationID { get; set; }

        [ForeignKey("POSConfigurationID")]
        public POSConfiguration POSConfiguration { get; set; }

    }
}
