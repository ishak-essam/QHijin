using BackEndHagan.Entities;
using BackEndHagan.Models.Dto;
using BackEndHagan.Services.IService;
using BackEndHagan.Services.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEndHagan.Controllers
{
    [ApiController]
    [Route ("api/terms_conditions")]
    public class TermsAndConditionsController : ControllerBase
    {
        private ITermsAndConditionService _TermsAndConditionService ;

        public TermsAndConditionsController ( ITermsAndConditionService TermsAndConditionService )
        {

            _TermsAndConditionService = TermsAndConditionService;

        }

        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> GetTermsAndConditions ( )
        {
            ResponseDTO  emp= await _TermsAndConditionService.GetTermsAndConditions ();
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }


        //[HttpPost]
        //[Authorize]
        //public async Task<ActionResult<ResponseDTO>> AddTermsAndCondition ( [FromBody] TermsAndConditionDto TermsAndCondition )
        //{
        //    return _TermsAndConditionService.AddTermsAndCondition (TermsAndCondition);
        //}

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ResponseDTO>> UpdateTermsAndCondition (  TermsAndCondition TermsAndCondition )
        {
            ResponseDTO  emp= await _TermsAndConditionService.UpdateTermsAndCondition (TermsAndCondition);
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult<ResponseDTO>> RemoveTermsAndCondition ( int id )
        {
            ResponseDTO  emp= await _TermsAndConditionService.RemoveTermsAndCondition (id);
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }
    }
}
