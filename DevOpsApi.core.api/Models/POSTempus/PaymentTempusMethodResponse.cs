using DevOpsApi.core.api.Models.Abstract;
using System.Xml.Serialization;

namespace DevOpsApi.core.api.Models.POSTempus
{
    [XmlRoot("TTMESSAGE")]
    public class PaymentTempusMethodResponse : TempusPaymentResponse
    {
        [XmlElement("TTMSGTRANSUCCESS")]
        public string TTMSGTRANSUCCESS { get; set; } = string.Empty;

        [XmlElement("TTMSGTRANRESPMESSAGE")]
        public string TTMSGTRANRESPMESSAGE { get; set; } = string.Empty;

        [XmlElement("TTMSGPROFILE")]
        public string TTMSGPROFILE { get; set; } = string.Empty;

        [XmlElement("TTMSGCHAINCODE")]
        public string TTMSGCHAINCODE { get; set; } = string.Empty;

        [XmlElement("TTMSGSERVERDATE")]
        public string TTMSGSERVERDATE { get; set; } = string.Empty;

        [XmlElement("TTMSGSERVERTIME")]
        public string TTMSGSERVERTIME { get; set; } = string.Empty;

        [XmlElement("TTMESSAGEERRORS")]
        public TTMESSAGEERRORS TTMESSAGEERRORS { get; set; }

        [XmlElement("TRANRESP")]
        public TRANRESP TRANRESP { get; set; } = new TRANRESP();

        [XmlElement("SESSIONID")]
        public string SESSIONID { get; set; } = string.Empty;

    }

    public class TRANRESP
    {
        [XmlElement("RESPTYPE")]
        public string RESPTYPE { get; set; } = string.Empty;

        [XmlElement("MANUALKEYCAPABLE")]
        public string MANUALKEYCAPABLE { get; set; } = string.Empty;

        [XmlElement("MSRCAPABLE")]
        public string MSRCAPABLE { get; set; } = string.Empty;

        [XmlElement("EMVCAPABLE")]
        public string EMVCAPABLE { get; set; } = string.Empty;

        [XmlElement("CLESSEMVCAPABLE")]
        public string CLESSEMVCAPABLE { get; set; } = string.Empty;

        [XmlElement("CLESSMSRCAPABLE")]
        public string CLESSMSRCAPABLE { get; set; } = string.Empty;

        [XmlElement("BARCODECAPABLE")]
        public string BARCODECAPABLE { get; set; } = string.Empty;

        [XmlElement("OCRCAPABLE")]
        public string OCRCAPABLE { get; set; } = string.Empty;

        [XmlElement("QRCODECAPABLE")]
        public string QRCODECAPABLE { get; set; } = string.Empty;

        [XmlElement("PINCAPABLE")]
        public string PINCAPABLE { get; set; } = string.Empty;

        [XmlElement("EMVRESPONSE")]
        public string EMVRESPONSE { get; set; } = string.Empty;

        [XmlElement("CCSYSTEMCODE")]
        public string CCSYSTEMCODE { get; set; } = string.Empty;

        [XmlElement("CCNONAUTHRESP")]
        public string CCNONAUTHRESP { get; set; } = string.Empty;

        [XmlElement("AUTHDATE")]
        public string AUTHDATE { get; set; } = string.Empty;

        [XmlElement("INTERACTIVERESPTYPE")]
        public string INTERACTIVERESPTYPE { get; set; } = string.Empty;

        [XmlElement("CCAUTHORIZED")]
        public string CCAUTHORIZED { get; set; } = string.Empty;

        [XmlElement("REQUESTEDAMOUNT")]
        public decimal REQUESTEDAMOUNT { get; set; } = 0m;

        [XmlElement("AUTHORIZEDAMOUNT")]
        public decimal AUTHORIZEDAMOUNT { get; set; } = 0m;

        [XmlElement("TRANIDENTGUID")]
        public string TRANIDENTGUID { get; set; } = string.Empty;

        [XmlElement("CCAMT")]
        public decimal CCAMT { get; set; } = 0m;

        [XmlElement("CCAVS")]
        public string CCAVS { get; set; } = string.Empty;

        [XmlElement("CCAVSRESP")]
        public string CCAVSRESP { get; set; } = string.Empty;

        [XmlElement("CCAUTHCODE")]
        public string CCAUTHCODE { get; set; } = string.Empty;

        [XmlElement("CCSALETYPE")]
        public string CCSALETYP { get; set; } = string.Empty;

        [XmlElement("ENTRYMODE")]
        public string ENTRYMODE { get; set; } = string.Empty;

        [XmlElement("CCTERMINALPROMPTRESP")]
        public string CCTERMINALPROMPTRESP { get; set; } = string.Empty;

        [XmlElement("TERMINALLANEIDENT")]
        public string TERMINALLANEIDENT { get; set; } = string.Empty;

        [XmlElement("CCAUTHRESPERRORCODE")]
        public string CCAUTHRESPERRORCODE { get; set; } = string.Empty;

        [XmlElement("TAXAMOUNT")]
        public decimal TAXAMOUNT { get; set; } = 0m;

        [XmlElement("QUICKCHIP")]
        public string QUICKCHIP { get; set; } = string.Empty;

        [XmlElement("EMVAID")]
        public string EMVAID { get; set; } = string.Empty;

        [XmlElement("SIGNATUREREQUIRED")]
        public string SIGNATUREREQUIRED { get; set; } = string.Empty;

        [XmlElement("CVMMODE")]
        public string CVMMODE { get; set; } = string.Empty;

        [XmlElement("EMVRECEIPTREQ")]
        public string EMVRECEIPTREQ { get; set; } = string.Empty;

        [XmlElement("LINEITEMSPRESENT")]
        public string LINEITEMSPRESENT { get; set; } = string.Empty;

        [XmlElement("ISSUERTRANID")]
        public string ISSUERTRANID { get; set; } = string.Empty;

        [XmlElement("STORENAME")]
        public string STORENAME { get; set; } = string.Empty;

        [XmlElement("STOREADDRESS")]
        public string STOREADDRESS { get; set; } = string.Empty;

        [XmlElement("STORECITY")]
        public string STORECITY { get; set; } = string.Empty;

        [XmlElement("STORESTATE")]
        public string STORESTATE { get; set; } = string.Empty;

        [XmlElement("STOREZIP")]
        public string STOREZIP { get; set; } = string.Empty;

        [XmlElement("STOREMID")]
        public string STOREMID { get; set; } = string.Empty;

        [XmlElement("STORETID")]
        public string STORETID { get; set; } = string.Empty;

        [XmlElement("STOREPHONE")]
        public string STOREPHONE { get; set; } = string.Empty;

        [XmlElement("PROCESSORTOKEN")]
        public string PROCESSORTOKEN { get; set; } = string.Empty;

        [XmlElement("TRANSARMORTOKEN")]
        public string TRANSARMORTOKEN { get; set; } = string.Empty;

        [XmlElement("CCEXP")]
        public string CCEXP { get; set; } = string.Empty;

        [XmlElement("TRANSARMORTOKENTYPE")]
        public string TRANSARMORTOKENTYPE { get; set; } = string.Empty;

        [XmlElement("TOKENCARDBRAND")]
        public string TOKENCARDBRAND { get; set; } = string.Empty;

        [XmlElement("DEBITONLYTOKEN")]
        public string DEBITONLYTOKEN { get; set; } = string.Empty;

        [XmlElement("TRANSARMORPROVIDER")]
        public string TRANSARMORPROVIDER { get; set; } = string.Empty;

        [XmlElement("SERVICENAME")]
        public string SERVICENAME { get; set; } = string.Empty;

        [XmlElement("TRANIDENT")]
        public string TRANIDENT { get; set; } = string.Empty;

        [XmlElement("STATIONIDENT")]
        public string STATIONIDENT { get; set; } = string.Empty;

        [XmlElement("CUSTIDENT")]
        public string CUSTIDENT { get; set; } = string.Empty;

        [XmlElement("EMPIDENT")]
        public string EMPIDENT { get; set; } = string.Empty;

        [XmlElement("CCACCOUNT")]
        public string CCACCOUNT { get; set; } = string.Empty;

        [XmlElement("WAVQLAQA")]
        public string WAVQLAQA { get; set; } = string.Empty;

        [XmlElement("CCCARDTYPE")]
        public string CCCARDTYPE { get; set; } = string.Empty;

        [XmlElement("CCCARDTYPEEX")]
        public string CCCARDTYPEEX { get; set; } = string.Empty;

        [XmlElement("SANITIZEDPAN")]
        public string SANITIZEDPAN { get; set; } = string.Empty;


        [XmlElement("RECEIPT")]
        public RECEIPT RECEIPT { get; set; }

        [XmlElement("TRANSUCCESS")]
        public string TRANSUCCESS { get; set; }

        [XmlElement("TRANRESPMESSAGE")]
        public string TRANRESPMESSAGE { get; set; }

        [XmlElement("PROFILE")]
        public string PROFILE { get; set; }

        [XmlElement("SERVERDATE")]
        public string SERVERDATE { get; set; }

        [XmlElement("SERVERTIME")]
        public string SERVERTIME { get; set; }

        [XmlElement("SIGSTATUS")]
        public string SIGSTATUS { get; set; }

        [XmlElement("SIGDATATYPE")]
        public int SIGDATATYPE { get; set; }

        [XmlElement("SIGDATA")]
        public string SIGDATA { get; set; }
    }

    public class RECEIPT
    {
        [XmlElement("RECEIPTLINES")]
        public int RECEIPTLINES { get; set; }

        [XmlElement("RECEIPT1")]
        public string RECEIPT1 { get; set; }

        [XmlElement("RECEIPT2")]
        public string RECEIPT2 { get; set; }

        [XmlElement("RECEIPT3")]
        public string RECEIPT3 { get; set; }

        [XmlElement("RECEIPT4")]
        public string RECEIPT4 { get; set; }

        [XmlElement("RECEIPT5")]
        public string RECEIPT5 { get; set; }

        [XmlElement("RECEIPT6")]
        public string RECEIPT6 { get; set; }

        [XmlElement("RECEIPT7")]
        public string RECEIPT7 { get; set; }

        [XmlElement("RECEIPT8")]
        public string RECEIPT8 { get; set; }

        [XmlElement("RECEIPT9")]
        public string RECEIPT9 { get; set; }

        [XmlElement("RECEIPT10")]
        public string RECEIPT10 { get; set; }

        [XmlElement("RECEIPT11")]
        public string RECEIPT11 { get; set; }

        [XmlElement("RECEIPT12")]
        public string RECEIPT12 { get; set; }

        [XmlElement("RECEIPT13")]
        public string RECEIPT13 { get; set; }

        [XmlElement("RECEIPT14")]
        public string RECEIPT14 { get; set; }

        [XmlElement("RECEIPT15")]
        public string RECEIPT15 { get; set; }

        [XmlElement("RECEIPT16")]
        public string RECEIPT16 { get; set; } = string.Empty;

        [XmlElement("RECEIPT17")]
        public string RECEIPT17 { get; set; } = string.Empty;

        [XmlElement("RECEIPT18")]
        public string RECEIPT18 { get; set; } = string.Empty;

        [XmlElement("RECEIPT19")]
        public string RECEIPT19 { get; set; } = string.Empty;

        [XmlElement("RECEIPT20")]
        public string RECEIPT20 { get; set; } = string.Empty;

        [XmlElement("RECEIPT21")]
        public string RECEIPT21 { get; set; } = string.Empty;

        [XmlElement("RECEIPT22")]
        public string RECEIPT22 { get; set; } = string.Empty;

        [XmlElement("RECEIPT23")]
        public string RECEIPT23 { get; set; } = string.Empty;

        [XmlElement("RECEIPT24")]
        public string RECEIPT24 { get; set; } = string.Empty;

        [XmlElement("RECEIPT25")]
        public string RECEIPT25 { get; set; } = string.Empty;

        [XmlElement("RECEIPT26")]
        public string RECEIPT26 { get; set; } = string.Empty;

        [XmlElement("RECEIPT27")]
        public string RECEIPT27 { get; set; } = string.Empty;

        [XmlElement("RECEIPT28")]
        public string RECEIPT28 { get; set; } = string.Empty;

        [XmlElement("RECEIPT29")]
        public string RECEIPT29 { get; set; } = string.Empty;

        [XmlElement("RECEIPT30")]
        public string RECEIPT30 { get; set; } = string.Empty;

        [XmlElement("RECEIPT31")]
        public string RECEIPT31 { get; set; } = string.Empty;

        [XmlElement("RECEIPT32")]
        public string RECEIPT32 { get; set; } = string.Empty;

        [XmlElement("RECEIPT33")]
        public string RECEIPT33 { get; set; } = string.Empty;

        [XmlElement("RECEIPT34")]
        public string RECEIPT34 { get; set; } = string.Empty;

        [XmlElement("RECEIPT35")]
        public string RECEIPT35 { get; set; } = string.Empty;

        [XmlElement("RECEIPT36")]
        public string RECEIPT36 { get; set; } = string.Empty;

        [XmlElement("RECEIPT37")]
        public string RECEIPT37 { get; set; } = string.Empty;

        [XmlElement("RECEIPT38")]
        public string RECEIPT38 { get; set; } = string.Empty;

        [XmlElement("RECEIPT39")]
        public string RECEIPT39 { get; set; } = string.Empty;

        [XmlElement("RECEIPT40")]
        public string RECEIPT40 { get; set; } = string.Empty;

        [XmlElement("RECEIPT41")]
        public string RECEIPT41 { get; set; } = string.Empty;

        [XmlElement("RECEIPT42")]
        public string RECEIPT42 { get; set; } = string.Empty;

        [XmlElement("RECEIPT43")]
        public string RECEIPT43 { get; set; } = string.Empty;

    }


    public class TTMESSAGEERRORS
    {

        [XmlElement("TTMESSAGEERROR")]
        public List<TTMESSAGEERROR> TTMESSAGEERROR { get; set; }
    }


    public class TTMESSAGEERROR
    {

        [XmlElement("ERRORCODE")]
        public int ERRORCODE { get; set; }


        [XmlElement("ERRORDESCRIPTION")]
        public string ERRORDESCRIPTION { get; set; }
    }
}
