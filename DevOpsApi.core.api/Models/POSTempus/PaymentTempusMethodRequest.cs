namespace DevOpsApi.core.api.Models.POSTempus
{
    public class PaymentTempusMethodRequest
    {
        public int TTMESSAGETIMEOUT { get; set; } = 3800000;

        public AUTHINFO AUTHINFO { get; set; } = new AUTHINFO();

        public TRANSACTION TRANSACTION { get; set; } = new TRANSACTION();
    }

    public class AUTHINFO
    {
        public string SUBSCRIBERKEY { get; set; } = string.Empty;

        public string RNID { get; set; } = string.Empty;

        public string RNCERT { get; set; } = string.Empty;
    }

    public class TRANSACTION
    {
        public string TRANSACTIONTYPE { get; set; } = string.Empty;

        public decimal CREDITAMT { get; set; }

        public string SIGPROMPT { get; set; } = string.Empty;

        public string RETURNSIG { get; set; } = string.Empty;

        /// <summary>
        /// CUSTOMER IDENTIFIER
        /// </summary>
        public string CUSTIDENT { get; set; } = string.Empty;

        /// <summary>
        /// EMPLOYEE IDENTIFIER
        /// </summary>
        public string EMPIDENT { get; set; } = string.Empty;

        public string STATIONIDENT { get; set; } = string.Empty;

        /// <summary>
        /// MERCHANT-UNIQUE-TRANSACTION-IDENTIFIER
        /// </summary>
        public string TRANIDENTGUID { get; set; } = string.Empty;

        /// <summary>
        /// Start of Level 2 Data
        /// </summary>
        public string TRANIDENT { get; set; } = string.Empty;

        public decimal TAXAMOUNT { get; set; }

        /// <summary>
        /// Start of Level 3 Data
        /// </summary>
        public LINEITEMS LINEITEMS { get; set; } = new LINEITEMS();

        /// <summary>
        /// Timeout time of each screen on the terminal in seconds
        /// </summary>
        public int INTERACTIVETIMEOUT { get; set; }

        public string ENCODING { get; set; } = string.Empty;

        public string CCAUTHTYPE { get; set; }
    }

    public class LINEITEMS
    {
        public List<LINEITEM> LINEITEM { get; set; } = new List<LINEITEM>();
    }

    public class LINEITEM
    {
        public string ITEMCOMMODITYCODE { get; set; } = string.Empty;

        public string ITEMDESCRIPTION { get; set; } = string.Empty;

        public string ITEMPRODUCTCODE { get; set; } = string.Empty;

        public int ITEMQUANTITY { get; set; }

        public string ITEMUNITOFMEASURE { get; set; } = string.Empty;

        public decimal ITEMUNITCOST { get; set; }

        public decimal ITEMTAXRATE { get; set; }

        public decimal ITEMTAXAMOUNT { get; set; }
    }
}
