namespace DevOpsApi.core.api.Models
{
    public class POSDeviceConfigurationModel : BaseModel
    {
        public int POSDeviceConfigurationID { get; set; }

        public string Subscriberkey { get; set; }

        public string HostName { get; set; }

        public string DeviceAlias { get; set; }

        public int Active { get; set; }

        public long CompanyID { get; set; }

        public long CompanyDepartmentID { get; set; }

        public int POSConfigurationID { get; set; }

        public POSConfigurationModel POSConfiguration { get; set; }

        public int IsDefault { get; set; } = 0;

        public long EmpID { get; set; }
    }
}
