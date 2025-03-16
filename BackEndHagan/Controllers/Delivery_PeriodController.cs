using BackEndHagan.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QHijin.Models.Dto;
using QHijin.Services.IService;

namespace QHijin.Controllers
{
    [Route("api/delivery_period")]
    [ApiController]
    public class Delivery_PeriodController : ControllerBase
    {
        private IDelivery_PeriodService _Delivery_PeriodService;

        public Delivery_PeriodController(IDelivery_PeriodService Delivery_PeriodService)
        {
            _Delivery_PeriodService = Delivery_PeriodService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> GetDelivery_Periods()
        {
            ResponseDTO emp = await _Delivery_PeriodService.GetDelivery_Periods();
            if (emp.IsSuccess == false)
                return BadRequest(emp);
            return Ok(emp);
        }



        //[HttpPost]
        //[Authorize]

        // public async Task<ActionResult<ResponseDTO>> AddDelivery_Period (  Delivery_PeriodDto Delivery_Period )
        //{
        //    return _Delivery_PeriodService.AddDelivery_Period (Delivery_Period);
        //}

        [HttpPost]
        [Authorize]

        public async Task<ActionResult<ResponseDTO>> UpdateDelivery_Period(Delivery_PeriodDto Delivery_Period)
        {
            ResponseDTO emp = await _Delivery_PeriodService.UpdateDelivery_Period(Delivery_Period);
            if (emp.IsSuccess == false)
                return BadRequest(emp);
            return Ok(emp);
        }
        [HttpDelete]
        [Authorize]
        public async Task<ActionResult<ResponseDTO>> RemoveDelivery_Period(int id)
        {
            ResponseDTO emp = await _Delivery_PeriodService.RemoveDelivery_Period(id);
            if (emp.IsSuccess == false)
                return BadRequest(emp);
            return Ok(emp);
        }
    }
}
