using BackEndHagan.Entities;
using BackEndHagan.Models.Dto;
using BackEndHagan.Services.IService;
using BackEndHagan.Services.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEndHagan.Controllers
{
    [ApiController]
    [Route("api/contact")]
    public class ContactController : ControllerBase
    {
        private IContactService _typeService ;

        public ContactController ( IContactService typeService )
        {
            _typeService = typeService;
        }

        [HttpGet]
    [Authorize]

        public async  Task<ActionResult<ResponseDTO>> GetContacts ( )
        {
            ResponseDTO  emp=   await _typeService.GetContacts ();
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }

        [HttpGet ("{id}")]
        [Authorize]

        public async Task<ActionResult<ResponseDTO>> GetContactById ( int id )
        {
            ResponseDTO  emp=   await _typeService.GetContactById (id);
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }

        [HttpGet ("filterByUser")]
        [Authorize]

        public async Task<ActionResult<ResponseDTO>> GetContactByUser ( int id  )
        {
            ResponseDTO  emp=   await _typeService.GetContactByUser (id);
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }
        [HttpPost ]
        public async Task<ActionResult<ResponseDTO>> AddContact ( [FromBody] ContactDto ContactDto )
        {
            ResponseDTO  emp=   await _typeService.AddContact (ContactDto);
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }

        [HttpPut]
        [Authorize]

        public async Task<ActionResult<ResponseDTO>> UpdateContact ( [FromBody] ContactDto ContactDto )
        {
            ResponseDTO  emp=   await _typeService.UpdateContact (ContactDto);
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }
        [HttpDelete]
        [Authorize]

        public async Task<ActionResult<ResponseDTO>> RemoveContact ( int id )
        {
            ResponseDTO  emp=   await _typeService.RemoveContact (id);
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }


    }
}
