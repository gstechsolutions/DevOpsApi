using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DevOpsApi.core.api.Models.POSTempus
{
    public class LocationModel : BaseModel
    {
        public long LocatonID { get; set; }

        public string? LocationCode { get; set; }

        public string? LocationName { get; set; }

        public string? Address { get; set; }

        public bool? Active { get; set; }

        public string? RimproWarehouseCode { get; set; }

        public bool? IsPDC { get; set; }
    }
}
