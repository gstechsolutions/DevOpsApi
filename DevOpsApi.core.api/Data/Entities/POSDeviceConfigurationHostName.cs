using System.ComponentModel.DataAnnotations;

namespace DevOpsApi.core.api.Data.Entities
{
    public class POSDeviceConfigurationHostName
    {
        [Key]
        public int POSDeviceConfigurationID { get; set; }

        public string DeviceAlias { get; set; }

        public int IsDefault { get; set; }
    }
}
