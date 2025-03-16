using BackEndHagan.Entities;
using BackEndHagan.Models.Dto;
using BackEndHagan.Services.IService;
using BackEndHagan.Services.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEndHagan.Controllers
{
    [ApiController]
    [Authorize]
    [Route ("api/salary")]
    public class SalaryController : ControllerBase
    {
        private ISalaryService _salaryService ;

        public SalaryController ( ISalaryService salaryService )
        {
            _salaryService = salaryService;
        }

        [HttpGet]
         public async Task<ActionResult<ResponseDTO>> GetSalarys ( )
        {
            ResponseDTO  emp= await _salaryService.GetSalarys ();
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }

        [HttpGet ("{id}")]
         public async Task<ActionResult<ResponseDTO>> GetSalaryById ( int id )
        {
            ResponseDTO  emp= await _salaryService.GetSalaryById (id);
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }

        [HttpGet ("Emp")]
         public async Task<ActionResult<ResponseDTO>> GetSalaryByEmpId ( int id )
        {
            ResponseDTO  emp= await _salaryService.GetSalaryByEmpId (id);
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }
        [HttpPost]
         public async Task<ActionResult<ResponseDTO>> AddSalary ( SalaryDto salary )
        {
            ResponseDTO  emp= await _salaryService.AddSalary (salary);
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }

        [HttpPut]
         public async Task<ActionResult<ResponseDTO>> UpdateSalary (  SalaryDto salary )
        {
            ResponseDTO  emp= await _salaryService.UpdateSalary (salary);
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }
        [HttpDelete]
         public async Task<ActionResult<ResponseDTO>> RemoveSalary ( int id )
        {
            ResponseDTO  emp= await _salaryService.RemoveSalary (id);
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }
    }
}
