using BackEndHagan.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QHijin.Models.Dto;
using QHijin.Services.IService;

namespace QHijin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private IPaymentRequestService _PaymentRequestService;
        public PaymentController(IPaymentRequestService PaymentRequestService)
        {
            _PaymentRequestService = PaymentRequestService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> GetPayments()
        {
            ResponseDTO emp = await _PaymentRequestService.GetPayments();
            if (emp.IsSuccess == false)
                return BadRequest(emp);
            return Ok(emp);
        }
        [HttpPost]
        public async Task<ActionResult<ResponseDTO>> AddPayment(PaymentRequestDto PaymentDto)
        {
            if(PaymentDto.UserId == null && PaymentDto.TrainerId == null)
                return BadRequest("UserId or trainer must be requeired");
            ResponseDTO emp = await _PaymentRequestService.AddPaymentSend(PaymentDto);
            if (emp.IsSuccess == false)
                return BadRequest(emp);
            return Ok(emp);
        }
        [HttpPut]
        public async Task<ActionResult<ResponseDTO>> PaymentRecive(string RequestRef)
        {
            ResponseDTO emp = await _PaymentRequestService.PaymentRecive(RequestRef);
            if (emp.IsSuccess == false)
                return BadRequest(emp);
            return Ok(emp);
        }
    }
}
