using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DevOpsApi.core.api.Models.POSTempus
{
    public class PosFiltersModel : BaseModel
    {
        public string? SalesNo { get; set; }

        public string? SubscriberKey { get; set; }

        public string? HostName { get; set; }

        public long? EmployeeID { get; set; }
    }
}
