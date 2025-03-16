using BackEndHagan.Extensions;
using BackEndHagan.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QHijin.Models.Dto;
using QHijin.ContactUs.IContactUs;

namespace QHijin.Controllers
{
    [ApiController]
    [Route("api/contactUs")]
    public class ContactUsController : ControllerBase
    {
        private IContactUsService _ContactUsService;

        public ContactUsController(IContactUsService ContactUsService)
        {
            _ContactUsService = ContactUsService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> GetContactUss()
        {
            ResponseDTO emp = await _ContactUsService.GetContactUs();
            if (emp.IsSuccess == false)
                return BadRequest(emp);
            return Ok(emp);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDTO>> GetContactUsById(int id)
        {
            if (User.GetRole()==0) return Unauthorized();
            ResponseDTO emp = await _ContactUsService.GetContactUsById(id);
            if (emp.IsSuccess == false)
                return BadRequest(emp);
            return Ok(emp);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ResponseDTO>> AddContactUs(ContactUsDto ContactUsDto)
        {
            if (User.GetRole()==0) return Unauthorized();
            ResponseDTO emp = await _ContactUsService.AddContactUs(ContactUsDto);
            if (emp.IsSuccess == false)
                return BadRequest(emp);
            return Ok(emp);
        }

        [HttpPut]
        [Authorize]

        public async Task<ActionResult<ResponseDTO>> UpdateContactUs(ContactUsDto ContactUsDto)
        {
            if (User.GetRole()==0) return Unauthorized();
            ResponseDTO emp = await _ContactUsService.UpdateContactUs(ContactUsDto);
            if (emp.IsSuccess == false)
                return BadRequest(emp);
            return Ok(emp);
        }
        [HttpDelete]
        [Authorize]

        public async Task<ActionResult<ResponseDTO>> RemoveContactUs(int id)
        {
            if (User.GetRole()==0) return Unauthorized();
            ResponseDTO emp = await _ContactUsService.RemoveContactUs(id);
            if (emp.IsSuccess == false)
                return BadRequest(emp);
            return Ok(emp);
        }
    }
}

