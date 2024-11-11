namespace DevOpsApi.core.api.Models
{
    public class POSDeviceConfigurationHostNameModel : BaseModel
    {
        public int POSDeviceConfigurationID { get; set; }

        public string DeviceAlias { get; set; }

        public int IsDefault { get; set; }
    }
}
