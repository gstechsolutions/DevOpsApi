using DevOpsApi.core.api.Models.POSTempus;
using DevOpsApi.core.api.Models;

namespace DevOpsApi.core.api.Services.POSTempus
{
    public interface ITempusService
    {
        Task<PaymentTempusMethodResponse> PaymentTempusMethods_Select(PaymentTempusMethodRequest tempusReq);

        Task<CorcentricTempusPaymentResponse> PaymentCorcentricTempusMethods_Select(CorcentricTempusPaymentRequest tempusReq);

        Task<string> GenerateSignature(string sigdata, string fileName);

        Task<List<LocationModel>> GetLocations();

        Task<List<PosInvoiceModel>> GetSIPPosInvoices(PosFiltersModel filters);

        Task<List<SISPosInvoiceModel>> GetSISPosInvoices(PosFiltersModel filters);

        //this is the one use for credut auth sales
        Task<PaymentTempusMethodResponse> PaymentCreditTempusMethods_Select(PaymentTempusMethodRequest tempusReq);

        Task<InteractiveCancelTempusResponse> InteractiveCancelTempusMethods_Select(InteractiveCancelTempusRequest tempusReq);

        Task<PosFiltersModel> CancelHttpClientRequest(PosFiltersModel filters);

        Task<List<POSDeviceConfigurationModel>> GetPOSDeviceConfigurationByHostName(PosFiltersModel filters);

        Task<POSDeviceConfigurationModel> SetPOSDeviceInLoginDetails(POSDeviceConfigurationModel model);

        Task<POSLoginDetailsModel> GetLoginDetailsByUser(PosFiltersModel model);

    }
}
