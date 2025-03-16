using BackEndHagan.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QHijin.Models.Dto;
using QHijin.Services.IService;

namespace QHijin.Controllers
{
    [Route("api/policy_refund")]
    [ApiController]
    public class Policy_RefundController : ControllerBase
    {
        private IPolicy_RefundService _Policy_RefundService;
        public Policy_RefundController(IPolicy_RefundService Policy_RefundService)
        {
            _Policy_RefundService = Policy_RefundService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> GetPolicy_Refunds()
        {
            ResponseDTO emp = await _Policy_RefundService.GetPolicy_Refunds();
            if (emp.IsSuccess == false)
                return BadRequest(emp);
            return Ok(emp);
        }



        //[HttpPost]
        //[Authorize]

        // public async Task<ActionResult<ResponseDTO>> AddPolicy_Refund (  Policy_RefundDto Policy_Refund )
        //{
        //    return _Policy_RefundService.AddPolicy_Refund (Policy_Refund);
        //}

        [HttpPost]
        [Authorize]

        public async Task<ActionResult<ResponseDTO>> UpdatePolicy_Refund(Policy_RefundDto Policy_Refund)
        {
            ResponseDTO emp = await _Policy_RefundService.UpdatePolicy_Refund(Policy_Refund);
            if (emp.IsSuccess == false)
                return BadRequest(emp);
            return Ok(emp);
        }
        [HttpDelete]
        [Authorize]
        public async Task<ActionResult<ResponseDTO>> RemovePolicy_Refund(int id)
        {
            ResponseDTO emp = await _Policy_RefundService.RemovePolicy_Refund(id);
            if (emp.IsSuccess == false)
                return BadRequest(emp);
            return Ok(emp);
        }
    }
}
