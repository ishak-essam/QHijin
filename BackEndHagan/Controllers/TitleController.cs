using BackEndHagan.Entities;
using BackEndHagan.Extensions;
using BackEndHagan.Models.Dto;
using BackEndHagan.Services.IService;
using BackEndHagan.Services.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackEndHagan.Controllers
{
    [Route ("api/title")]
    [ApiController]
    public class TitleController : ControllerBase
    {
        private ITitleService _TitleService ;

        public TitleController ( ITitleService TitleService )
        {
            _TitleService = TitleService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> GetTitles ( )
        {
            ResponseDTO  emp= await  _TitleService.GetTitles ();
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }

        [HttpGet ("{id}")]
        public async Task<ActionResult<ResponseDTO>> GetTitleById ( int id )
        {
            ResponseDTO  emp= await _TitleService.GetTitleById (id);
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }

        [HttpGet ("GetByEMP")]
        public async Task<ActionResult<ResponseDTO>> GetTitleByEmp (  )
        {
            if ( User.GetRoles ().Contains (0) ) return Unauthorized ();
            ResponseDTO  emp=await _TitleService.GetTitleByEmp ();
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }
        [HttpGet ("GetByAdmin")]

        public async Task<ActionResult<ResponseDTO>> GetTitleByName ( )
        {
            if ( User.GetRoles ().Contains (0) ) return Unauthorized ();
            ResponseDTO  emp=await _TitleService.GetTitleByAdmin ();
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }
        [HttpPost]
        public async Task<ActionResult<ResponseDTO>>  AddTitle ( [FromBody] Title TitleDto )
        {
            if ( !User.GetRoles ().Contains (1) ) return Unauthorized ();
            ResponseDTO  emp= await _TitleService.AddTitle (TitleDto); 
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }

        [HttpPut]
        public async Task<ActionResult<ResponseDTO>> UpdateTitle ( [FromBody] Title TitleDto )
        {
            if ( User.GetRoles ().Contains (0) ) return Unauthorized ();
            ResponseDTO  emp=await _TitleService.UpdateTitle (TitleDto);
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }
        [HttpDelete]
        public async Task<ActionResult<ResponseDTO>> RemoveTitle ( int id )
        {
            if ( User.GetRoles ().Contains (0) ) return Unauthorized ();
            ResponseDTO  emp=await _TitleService.RemoveTitle (id);
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }
    }
}