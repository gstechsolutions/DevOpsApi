using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DevOpsApi.core.api.Data.Entities
{
    public class SISPosInvoice
    {
        [Key]
        public long SalesId { get; set; }
        public string SalesNo { get; set; }
        public long CompanyId { get; set; }
        public string CompanyName { get; set; }
        public long CompanyDepartmentID { get; set; }
        public string CustomerName { get; set; }
        public decimal Parts { get; set; }
        public decimal PartsQty { get; set; }

        public decimal Freight { get; set; }

        public decimal Labor { get; set; }
        public decimal Sublet { get; set; }

        [Column("Additional Charges")]
        public decimal AdditionalCharges { get; set; }

        public decimal Paint { get; set; }

        public decimal PaintQty { get; set; }

        public decimal LaborQty { get; set; }

        public decimal SubletQty { get; set; }

        public decimal FreightQty { get; set; }

        public decimal ServiceAddChargeQty { get; set; }

    }
}
