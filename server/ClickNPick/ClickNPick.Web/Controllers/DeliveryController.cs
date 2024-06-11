using ClickNPick.Application.Services.Delivery;
using ClickNPick.Web.Extensions;
using ClickNPick.Web.Models.Delivery.Request;
using ClickNPick.Web.Models.Delivery.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClickNPick.Web.Controllers
{
    public class DeliveryController : ApiController
    {
        private readonly IDeliveryService _deliveryService;

        public DeliveryController(
            IDeliveryService deliveryService)
        {
            _deliveryService = deliveryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCountries()
        {
            var result = await _deliveryService.GetCountriesAsync();
            var response = CountriesResponseModel.FromCountriesResponseModelDto(result);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetCities([FromQuery] GetCitiesRequestModel model)
        {
            var dto = model.ToGetCitiesRequestDto();
            var result = await _deliveryService.GetCitiesAsync(dto);
            var response = CitiesResponseModel.FromCitiesResponseDto(result);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult?> CreateLabel([FromBody] CreateLabelRequestModel requestModel)
        {
            var dto = requestModel.ToCreateLabelRequestDto();
            var result = await _deliveryService.CreateLabelAsync(dto);
            var response = CreateLabelResponseModel.FromCreateLabelResponseDto(result);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult?> DeleteLabel([FromQuery] DeleteLabelsRequestModel requestModel)
        {
            var dto = requestModel.ToDeleteLabelsRequestDto();
            var result = await _deliveryService.DeleteLabelsAsync(dto);
            var response = DeleteLabelsResponseModel.FromDeleteLabelsResponseDto(result);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetOffices([FromQuery] GetOfficesRequestModel inputModel)
        {
            var dto = inputModel.ToGetOfficesRequestDto();
            var result = await _deliveryService.GetOfficesAsync(dto);
            var response = OfficesResponseModel.FromOfficesResponseDto(result);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> ShipmentStatuses([FromQuery] GetShipmentStatusesRequestModel requestModel)
        {
            var dto = requestModel.ToGetShipmentStatusesRequestDto();
            var result = await _deliveryService.GetShipmentStatusesAsync(dto);
            var response = GetShipmentStatusesResponseModel.FromGetShipmentStatusesResponseDto(result);
            return Ok(response);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RequestShipment(RquestShipment model)
        {
            var userId = HttpContext.User.GetId();
            var dto = model.ToRequestShipmentRequestDto();
            dto.BuyerId = userId;
            await _deliveryService.CreateShipmentRequestAsync(dto);

            return Ok();
        }

        [Authorize]
        [HttpGet]      
        public async Task<IActionResult> ShipmentsToReceive()
        {
            var userId = HttpContext.User.GetId();
            
            var result = await _deliveryService.GetShipmentsToReceiveAsync(userId);
            var response = ShipmentListingResponseModel.FromShipmentListingResponseDto(result);
            
            return Ok(response);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ShipmentsToSend()
        {
            var userId = HttpContext.User.GetId();

            var result = await _deliveryService.GetShipmentsToSendAsync(userId);
            var response = ShipmentListingResponseModel.FromShipmentListingResponseDto(result);

            return Ok(response);
        }
    }
}
