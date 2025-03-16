using BackEndHagan.Entities;
using BackEndHagan.Extensions;
using BackEndHagan.Models.Dto;
using BackEndHagan.Services.IService;
using BackEndHagan.Services.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEndHagan.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/type")]
    public class TypeController : ControllerBase
    {
        private ITypeService _TypeService ;

        public TypeController ( ITypeService TypeService )
        {
            _TypeService = TypeService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> GetTypes ( )
        {
            ResponseDTO  emp=await _TypeService.GetTypes ();
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }

        [HttpGet ("{id}")]
        public async Task<ActionResult<ResponseDTO>> GetTypeById ( int id )
        {
            if ( User.GetRoles ().Contains (0) ) return Unauthorized ();
            ResponseDTO  emp=await _TypeService.GetTypeById (id);
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }

        [HttpGet ("filterByName")]
        public async Task<ActionResult<ResponseDTO>> GetTypeByName ( string name = "" )
        {
            if ( User.GetRoles ().Contains (0) ) return Unauthorized ();
            ResponseDTO  emp=await _TypeService.GetTypeByName (name);
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }
        [HttpPost ]
        public async Task<ActionResult<ResponseDTO>> AddType (  TypeDto TypeDto )
        {
            if ( User.GetRoles ().Contains (0) ) return Unauthorized ();
            ResponseDTO  emp= await _TypeService.AddType (TypeDto);
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }

        [HttpPut]
        public async Task<ActionResult<ResponseDTO>> UpdateType (  TypeDto TypeDto )
        {
            if ( User.GetRoles ().Contains (0) ) return Unauthorized ();
            ResponseDTO  emp= await _TypeService.UpdateType (TypeDto);
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }
        [HttpDelete]
        public async Task<ActionResult<ResponseDTO>> RemoveType ( int id )
        {
            if ( User.GetRoles ().Contains (0) ) return Unauthorized ();
            ResponseDTO  emp=  await _TypeService.RemoveType (id);
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }
    }
}
