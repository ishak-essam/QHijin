using BackEndHagan.Models.Dto;
using BackEndHagan.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QHijin.Models.Dto;
using QHijin.Services.IService;

namespace QHijin.Controllers
{
    [Route("api/contracting_policy")]
    [ApiController]
    public class Contracting_PolicyController : ControllerBase
    {
        private IContracting_PolicyService _Contracting_PolicyService;

        public Contracting_PolicyController(IContracting_PolicyService Contracting_PolicyService)
        {
            _Contracting_PolicyService = Contracting_PolicyService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> GetContracting_Policys()
        {
            ResponseDTO emp = await _Contracting_PolicyService.GetContracting_Policys();
            if (emp.IsSuccess == false)
                return BadRequest(emp);
            return Ok(emp);
        }



        //[HttpPost]
        //[Authorize]

        // public async Task<ActionResult<ResponseDTO>> AddContracting_Policy (  Contracting_PolicyDto Contracting_Policy )
        //{
        //    return _Contracting_PolicyService.AddContracting_Policy (Contracting_Policy);
        //}

        [HttpPost]
        [Authorize]

        public async Task<ActionResult<ResponseDTO>> UpdateContracting_Policy(Contracting_PolicyDto Contracting_Policy)
        {
            ResponseDTO emp = await _Contracting_PolicyService.UpdateContracting_Policy(Contracting_Policy);
            if (emp.IsSuccess == false)
                return BadRequest(emp);
            return Ok(emp);
        }
        [HttpDelete]
        [Authorize]
        public async Task<ActionResult<ResponseDTO>> RemoveContracting_Policy(int id)
        {
            ResponseDTO emp = await _Contracting_PolicyService.RemoveContracting_Policy(id);
            if (emp.IsSuccess == false)
                return BadRequest(emp);
            return Ok(emp);
        }
    }
}
