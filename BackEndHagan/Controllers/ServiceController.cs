using BackEndHagan.Extensions;
using BackEndHagan.Models.Dto;
using BackEndHagan.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QHijin.Models.Dto;
using QHijin.Services.IService;

namespace QHijin.Controllers
{
    [ApiController]
    [Route("api/services")]
    public class ServiceController : ControllerBase
    {
        private IServices _ServiceService;

        public ServiceController(IServices ServiceService)
        {
            _ServiceService = ServiceService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> GetServices()
        {
            ResponseDTO emp = await _ServiceService.GetService();
            if (emp.IsSuccess == false)
                return BadRequest(emp);
            return Ok(emp);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDTO>> GetServiceById(int id)
        {
            if (User.GetRole()==0) return Unauthorized();
            ResponseDTO emp = await _ServiceService.GetServiceById(id);
            if (emp.IsSuccess == false)
                return BadRequest(emp);
            return Ok(emp);
        }

        [HttpPost]     
    [Authorize]
        public async Task<ActionResult<ResponseDTO>> AddService(ServiceDto ServiceDto)
        {
            if (User.GetRole()==0) return Unauthorized();
            ResponseDTO emp = await _ServiceService.AddService(ServiceDto);
            if (emp.IsSuccess == false)
                return BadRequest(emp);
            return Ok(emp);
        }

        [HttpPut]
        [Authorize]

        public async Task<ActionResult<ResponseDTO>> UpdateService(ServiceDto ServiceDto)
        {
            if (User.GetRole()==0) return Unauthorized();
            ResponseDTO emp = await _ServiceService.UpdateService(ServiceDto);
            if (emp.IsSuccess == false)
                return BadRequest(emp);
            return Ok(emp);
        }
        [HttpDelete]
        [Authorize]

        public async Task<ActionResult<ResponseDTO>> RemoveService(int id)
        {
            if (User.GetRole()==0) return Unauthorized();
            ResponseDTO emp = await _ServiceService.RemoveService(id);
            if (emp.IsSuccess == false)
                return BadRequest(emp);
            return Ok(emp);
        }
    }
}
