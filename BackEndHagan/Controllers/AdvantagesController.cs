using BackEndHagan.Extensions;
using BackEndHagan.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QHijin.Models.Dto;
using QHijin.Services.IService;

namespace QHijin.Controllers
{
    [Route("api/advantages")]
    [ApiController]
    public class AdvantagesController : ControllerBase
    {
        private IAdvantages _AdvantagesService;

        public AdvantagesController(IAdvantages AdvantagesService)
        {
            _AdvantagesService = AdvantagesService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> GetAdvantagess()
        {
            ResponseDTO emp = await _AdvantagesService.GetAdvantage();
            if (emp.IsSuccess == false)
                return BadRequest(emp);
            return Ok(emp);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDTO>> GetAdvantagesById(int id)
        {
            if (User.GetRole()==0) return Unauthorized();
            ResponseDTO emp = await _AdvantagesService.GetAdvantageById(id);
            if (emp.IsSuccess == false)
                return BadRequest(emp);
            return Ok(emp);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ResponseDTO>> AddAdvantages(AdvantageDto AdvantagesDto)
        {
            if (User.GetRole() == 0) return Unauthorized();
            ResponseDTO emp = await _AdvantagesService.AddAdvantage(AdvantagesDto);
            if (emp.IsSuccess == false)
                return BadRequest(emp);
            return Ok(emp);
        }

        [HttpPut]
        [Authorize]

        public async Task<ActionResult<ResponseDTO>> UpdateAdvantages(AdvantageDto AdvantagesDto)
        {
            if (User.GetRole()==0) return Unauthorized();
            ResponseDTO emp = await _AdvantagesService.UpdateAdvantage(AdvantagesDto);
            if (emp.IsSuccess == false)
                return BadRequest(emp);
            return Ok(emp);
        }
        [HttpDelete]
        [Authorize]

        public async Task<ActionResult<ResponseDTO>> RemoveAdvantages(int id)
        {
            if (User.GetRole()==0) return Unauthorized();
            ResponseDTO emp = await _AdvantagesService.RemoveAdvantage(id);
            if (emp.IsSuccess == false)
                return BadRequest(emp);
            return Ok(emp);
        }
    }
}

