using System.Xml.Serialization;

namespace DevOpsApi.core.api.Models.POSTempus
{
    [XmlRoot("TTMESSAGE")]
    public class CorcentricTempusPaymentRequest
    {
        [XmlElement("TTMESSAGETIMEOUT")]
        public int TTMESSAGETIMEOUT { get; set; }

        [XmlElement("AUTHINFO")]
        public AuthInfo AUTHINFO { get; set; }

        [XmlElement("TRANSACTION")]
        public Transaction TRANSACTION { get; set; }

        public CancellationTokenSource CancellationToken { get; set; }

        //public string ScreenType { get; set; }

        //public string InvoiceNumber { get; set; }
    }

    public class AuthInfo
    {
        [XmlElement("SUBSCRIBERKEY")]
        public string SUBSCRIBERKEY { get; set; }

        [XmlElement("RNID")]
        public string RNID { get; set; }

        [XmlElement("RNCERT")]
        public string RNCERT { get; set; }
    }

    public class Transaction
    {
        [XmlElement("TRANSACTIONTYPE")]
        public string TRANSACTIONTYPE { get; set; }

        //[XmlElement("LEGALTEXT")]
        //public string LEGALTEXT { get; set; }

        [XmlElement("SIGTYPE")]
        public string SIGTYPE { get; set; }
    }
}
