using BackEndHagan.Entities;
using BackEndHagan.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BackEndHagan.Services.IService;
using BackEndHagan.Extensions;
using BackEndHagan.Services.Service;

namespace BackEndHagan.Controllers
{
    [ApiController]
    [Authorize]
    [Route ("api/user")]
    public  class UserController : ControllerBase
    {
        private IUserService _userService ;

        public  UserController ( IUserService userService )
        {
            _userService = userService;
        }

        [HttpGet("/api/users")]
        public  async Task<ActionResult<ResponseDTO>> GetUsers ( )
        {
            if ( User.GetRoles().Contains (0) ) return Unauthorized ();
            ResponseDTO  emp=  await _userService.GetUsers ();
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }
        [HttpGet ]
        public async Task<ActionResult<ResponseDTO>> GetUserId (  )
        {
            ResponseDTO  emp=   await _userService.GetUserById (User.GetUserId ());
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }

        [HttpGet ("{id}")]
        public async Task<ActionResult<ResponseDTO>> GetUserById ( int id )
        {
            if ( User.GetRoles().Contains (0) ) return Unauthorized ();
            ResponseDTO  emp=    await _userService.GetUserById (id);
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }

        [HttpGet ("filterByName")]
        public async Task<ActionResult<ResponseDTO>>  GetUserByName ( string name = "" )
        {
            ResponseDTO  emp=  await _userService.GetUserByName (name);
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }
        [HttpGet ("GetUserByPhone")]
         public async Task<ActionResult<ResponseDTO>> GetUserByPhone ( string phone = "" )
        {
            ResponseDTO  emp=  await _userService.GetUserByPhone (phone);
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }

        [HttpPut]
        public async Task<ActionResult<ResponseDTO>> UpdateUser (  UserDTO2 UserDto )
        {
            ResponseDTO  emp=await _userService.UpdateUser (UserDto);
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }
        [HttpDelete]
        public async Task<ActionResult<ResponseDTO>> RomveUser (int id )
        {
           // if ( User.GetRoles ().Contains (0) ) return Unauthorized ();
            ResponseDTO  emp=await _userService.RemoveUser (id);
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }
        [HttpPost("renew")]
        public async Task<ActionResult<ResponseDTO>> RenewUser ( int id )
        {
            if ( User.GetRoles().Contains (0) ) return Unauthorized ();
            ResponseDTO  emp=await _userService.RenewUser (id);
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }


    }
}
