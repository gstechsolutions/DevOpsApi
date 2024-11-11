using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DevOpsApi.core.api.Models.POSTempus
{
    public class TempusDeviceConfigurationModel : BaseModel
    {
        public int POSConfigurationID { get; set; }

        public int RNID { get; set; }

        public string RNCert { get; set; }

        public long CompanyID { get; set; }

        public long CompanyDepartmentID { get; set; }

        public int POSDeviceConfigurationID { get; set; }

        public string Subscriberkey { get; set; }

        public string HostName { get; set; }

        public string DeviceAlias { get; set; }

        public int Active { get; set; }
    }
}
