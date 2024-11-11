namespace DevOpsApi.core.api.Models
{
    public class PosInvoiceModel : BaseModel
    {
        public long SalesId { get; set; }
        public string SalesNo { get; set; }
        public long CompanyId { get; set; }
        public string CompanyName { get; set; }
        public long CompanyDepartmentID { get; set; }
        public string CustomerName { get; set; }
        public decimal Parts { get; set; }
        public long PartsQty { get; set; }
        public decimal AdditionalCharges { get; set; }
        public long AdditionalChargesQty { get; set; }
        public decimal Freight { get; set; }
        public int CustomerTaxRate { get; set; }
        public int ItemTaxAmount { get; set; }

        public decimal Labor { get; set; }
        //public int Labor { get; set; }


    }
}
