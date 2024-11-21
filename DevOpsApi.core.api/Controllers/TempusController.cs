using DevOpsApi.core.api.Models;
using DevOpsApi.core.api.Models.POSTempus;
using DevOpsApi.core.api.Services.POSTempus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DevOpsApi.core.api.Controllers
{
    
    [ApiController]
    public class TempusController : ControllerBase
    {
        private readonly ITempusService service;

        public TempusController(ITempusService service)
        {
            this.service = service;
        }

        [HttpPost]
        [Route("api/tempus/saleauth")]
        [SwaggerOperation(OperationId = "PaymentTempusMethods_Select")]
        [SwaggerResponse(statusCode: 200, type: typeof(PaymentTempusMethodRequest), description: "Used to call Tempus for auth sale")]
        public async Task<IActionResult> PaymentTempusMethods_Select([FromBody] PaymentTempusMethodRequest order)
        {
            var response = await service.PaymentTempusMethods_Select(order);
            return Ok(response);
        }

        //public async Task<CorcentricTempusPaymentResponse> PaymentCorcentricTempusMethods_Select(CorcentricTempusPaymentRequest tempusReq)

        [HttpPost]
        [Route("api/tempus/pay/corcentric")]
        [SwaggerOperation(OperationId = "PaymentCorcentricTempusMethods_Select")]
        [SwaggerResponse(statusCode: 200, type: typeof(CorcentricTempusPaymentResponse), description: "Used to call Tempus for corcentric sale")]
        public async Task<IActionResult> PaymentCorcentricTempusMethods_Select([FromBody] CorcentricTempusPaymentRequest order)
        {
            var response = await service.PaymentCorcentricTempusMethods_Select(order);
            return Ok(response);
        }

        [Authorize]
        [HttpGet]
        [Route("api/tempus/location/list")]
        [SwaggerOperation(OperationId = "GetLocations")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<LocationModel>), description: "Used to retrieve list of locations.")]
        public async Task<IActionResult> GetLocations()
        {
            var list = await service.GetLocations();
            return Ok(list);
        }

        [HttpPost]
        [Route("api/tempus/invoice")]
        [SwaggerOperation(OperationId = "GetPosInvoices")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<PosInvoiceModel>), description: "Used to call Tempus for corcentric sale")]
        public async Task<IActionResult> GetPosInvoices([FromBody] PosFiltersModel invoice)
        {
            var response = await service.GetSIPPosInvoices(invoice);
            return Ok(response);
        }

        [HttpPost]
        [Route("api/tempus/invoice/sis")]
        [SwaggerOperation(OperationId = "GetSISPosInvoices")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<SISPosInvoiceModel>), description: "Used to call Tempus for corcentric sale")]
        public async Task<IActionResult> GetSISPosInvoices([FromBody] PosFiltersModel invoice)
        {
            var response = await service.GetSISPosInvoices(invoice);
            return Ok(response);
        }

        [HttpPost]
        [Route("api/tempus/pay/credit")]
        [SwaggerOperation(OperationId = "PaymentCreditTempusMethods_Select")]
        [SwaggerResponse(statusCode: 200, type: typeof(PaymentTempusMethodResponse), description: "Used to call Tempus for credit sale")]
        public async Task<IActionResult> PaymentCreditTempusMethods_Select([FromBody] PaymentTempusMethodRequest order)
        {
            var response = await service.PaymentCreditTempusMethods_Select(order);
            return Ok(response);
        }

        [HttpPost]
        [Route("api/tempus/cancel")]
        [SwaggerOperation(OperationId = "InteractiveCancelTempusMethods_Select")]
        [SwaggerResponse(statusCode: 200, type: typeof(InteractiveCancelTempusResponse), description: "Used to call Tempus to cancel request")]
        public async Task<IActionResult> InteractiveCancelTempusMethods_Select([FromBody] InteractiveCancelTempusRequest order)
        {
            var response = await service.InteractiveCancelTempusMethods_Select(order);
            return Ok(response);
        }

        [HttpPost]
        [Route("api/tempus/http/cancel")]
        [SwaggerOperation(OperationId = "CancelHttpClientRequest")]
        [SwaggerResponse(statusCode: 200, type: typeof(PosFiltersModel), description: "Used to call Tempus to cancel request")]
        public async Task<IActionResult> CancelHttpClientRequest([FromBody] PosFiltersModel filter)
        {
            var response = await service.CancelHttpClientRequest(filter);
            return Ok(response);
        }

        [HttpPost]
        [Route("api/tempus/default/host")]
        [SwaggerOperation(OperationId = "GetPOSDeviceConfigurationByHostName")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<POSDeviceConfigurationModel>), description: "Used to get the default device by host name")]
        public async Task<IActionResult> GetPOSDeviceConfigurationByHostName([FromBody] PosFiltersModel filter)
        {
            var response = await service.GetPOSDeviceConfigurationByHostName(filter);
            return Ok(response);
        }

        [HttpPost]
        [Route("api/tempus/set/device")]
        [SwaggerOperation(OperationId = "SetPOSDeviceInLoginDetails")]
        [SwaggerResponse(statusCode: 200, type: typeof(POSDeviceConfigurationModel), description: "Used to update the POSDeviceConfigurationID in the login details table")]
        public async Task<IActionResult> SetPOSDeviceInLoginDetails([FromBody] POSDeviceConfigurationModel filter)
        {
            var response = await service.SetPOSDeviceInLoginDetails(filter);
            return Ok(response);
        }

        [HttpPost]
        [Route("api/tempus/login/details")]
        [SwaggerOperation(OperationId = "GetLoginDetailsByUser")]
        [SwaggerResponse(statusCode: 200, type: typeof(POSLoginDetailsModel), description: "Used to get login details by empId")]
        public async Task<IActionResult> GetLoginDetailsByUser([FromBody] PosFiltersModel filter)
        {
            var response = await service.GetLoginDetailsByUser(filter);
            return Ok(response);
        }

        //https://jsonplaceholder.typicode.com/posts
        [Authorize(Roles = "dispatcher")]
        [HttpGet]
        [Route("api/tempus/posts/list")]
        [SwaggerOperation(OperationId = "GetPosts")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<LocationModel>), description: "Used to retrieve list of locations.")]
        public async Task<IActionResult> GetPosts()
        {
            var list = await service.GetPosts();
            return Ok(list);
        }               
    }
}
