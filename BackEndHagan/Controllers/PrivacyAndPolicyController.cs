using BackEndHagan.Entities;
using BackEndHagan.Models.Dto;
using BackEndHagan.Services.IService;
using BackEndHagan.Services.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEndHagan.Controllers
{
    [ApiController]
    [Route ("api/privacy_policy")]
    public class PrivacyAndPolicyController : ControllerBase
    {
        private IPrivacyAndPolicyService _PrivacyAndPolicyService ;

        public PrivacyAndPolicyController ( IPrivacyAndPolicyService PrivacyAndPolicyService )
        {
            _PrivacyAndPolicyService = PrivacyAndPolicyService;
        }

        [HttpGet]
         public async Task<ActionResult<ResponseDTO>> GetPrivacyAndPolicys ( )
        {
            ResponseDTO  emp= await _PrivacyAndPolicyService.GetPrivacyAndPolicys ();
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }



        //[HttpPost]
        //[Authorize]

        // public async Task<ActionResult<ResponseDTO>> AddPrivacyAndPolicy (  PrivacyAndPolicyDto PrivacyAndPolicy )
        //{
        //    return _PrivacyAndPolicyService.AddPrivacyAndPolicy (PrivacyAndPolicy);
        //}

        [HttpPost]
        [Authorize]

        public async Task<ActionResult<ResponseDTO>> UpdatePrivacyAndPolicy ( PrivacyAndPolicyDto PrivacyAndPolicy )
        {
            ResponseDTO  emp= await _PrivacyAndPolicyService.UpdatePrivacyAndPolicy (PrivacyAndPolicy);
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }
    [HttpDelete]
        [Authorize]
         public async Task<ActionResult<ResponseDTO>> RemovePrivacyAndPolicy ( int id )
        {
            ResponseDTO  emp= await _PrivacyAndPolicyService.RemovePrivacyAndPolicy (id);
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }
    }
}
