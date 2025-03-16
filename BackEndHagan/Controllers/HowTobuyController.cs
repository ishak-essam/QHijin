using BackEndHagan.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QHijin.Models.Dto;
using QHijin.Services.IService;

namespace QHijin.Controllers
{
    [Route("api/howTobuy")]
    [ApiController]
    public class HowTobuyController : ControllerBase
    {
        private IHowTobuyService _HowTobuyService;

        public HowTobuyController(IHowTobuyService HowTobuyService)
        {
            _HowTobuyService = HowTobuyService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> GetHowTobuys()
        {
            ResponseDTO emp = await _HowTobuyService.GetHowTobuys();
            if (emp.IsSuccess == false)
                return BadRequest(emp);
            return Ok(emp);
        }
        [HttpPost]
        [Authorize]

        public async Task<ActionResult<ResponseDTO>> UpdateHowTobuy(HowTobuyDto HowTobuy)
        {
            ResponseDTO emp = await _HowTobuyService.UpdateHowTobuy(HowTobuy);
            if (emp.IsSuccess == false)
                return BadRequest(emp);
            return Ok(emp);
        }
        [HttpDelete]
        [Authorize]
        public async Task<ActionResult<ResponseDTO>> RemoveHowTobuy(int id)
        {
            ResponseDTO emp = await _HowTobuyService.RemoveHowTobuy(id);
            if (emp.IsSuccess == false)
                return BadRequest(emp);
            return Ok(emp);
        }
    }
}
