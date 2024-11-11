namespace DevOpsApi.core.api.Models
{
    public class POSConfigurationModel : BaseModel
    {
        public int POSConfigurationID { get; set; }

        public int RNID { get; set; }

        public string RNCert { get; set; }

        public long CompanyID { get; set; }

        public long CompanyDepartmentID { get; set; }
    }
}
