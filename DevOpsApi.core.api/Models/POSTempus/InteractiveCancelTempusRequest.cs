using System.Xml.Serialization;

namespace DevOpsApi.core.api.Models.POSTempus
{
    [XmlRoot("TTMESSAGE")]
    public class InteractiveCancelTempusRequest
    {

        public int TTMESSAGETIMEOUT { get; set; }


        public AuthInfo AUTHINFO { get; set; }


        public InteractiveCancelTransaction TRANSACTION { get; set; }
    }

    public class InteractiveCancelTransaction
    {

        public string TRANSACTIONTYPE { get; set; }


    }
}
