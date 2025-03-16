using AutoMapper;
using BackEndHagan.Entities;
using BackEndHagan.Extensions;
using BackEndHagan.Models.Dto;
using BackEndHagan.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEndHagan.Controllers
{
    [ApiController]
    [Route("api/employee")]
    public class EmployeeController : ControllerBase
    {
        private ResponseDTO _responseDTO;
        private HaganContext _haganContext;
        private IJwtTokenGenerator _jwtTokenGenerator;
        private IEmployeeService _EmployeeService;
        private IMapper _mapper;
        public EmployeeController(IMapper mapper, IJwtTokenGenerator jwtTokenGenerator, IEmployeeService EmployeeService, HaganContext haganContext)
        {
            _EmployeeService = EmployeeService;
            _mapper = mapper;
            _haganContext = haganContext;
            _jwtTokenGenerator = jwtTokenGenerator;
            _responseDTO = new ResponseDTO();
        }


        //[HttpPost ("login")]
        //public async Task<ActionResult<ResponseDTO>> login ( LoginRequestDTO employee )
        //{

        //    var user=  _haganContext.Employees
        //.FirstOrDefault(x=> x.Email.ToLower()==employee.Email.ToLower());
        //    if ( user == null ) return Unauthorized ("invalid username");

        //    var Admin= _haganContext.Admins.FirstOrDefault(u=>u.EmpId==user.EmpId);
        //    var admin=true;
        //    using var hmac=new HMACSHA512(user.PasswordSalt);
        //    var ComputedHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(employee.Password));
        //    for ( int i = 0; i < ComputedHash.Length; i++ )
        //    {
        //        if ( ComputedHash [ i ] != user.PasswordHash [ i ] )
        //            return Unauthorized ("Invalid password");
        //    }
        //    if ( Admin != null ) admin = true;
        //    else admin = false;
        //    var token= _jwtTokenGenerator.GenerateTokenDashboard (user,admin);
        //    LoginResponseDTO loginRequestDTO=new LoginResponseDTO()
        //    {
        //        Employee=_mapper.Map<EmployeeDto>(user),
        //        Token=token
        //    };
        //    return Ok (loginRequestDTO);
        //}

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ResponseDTO>> AddEmployee(EmployeeDto Add)
        {
            if (User.GetRoles().Contains(0)) return Unauthorized();

            ResponseDTO emp = await _EmployeeService.RegisterEmp(Add);
            if (emp.IsSuccess == false)
                return BadRequest(emp);
            return Ok(emp);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<ResponseDTO>> GetEmployees()
        {
            if (User.GetRole() == 0 && User.GetRole() == 3) return Unauthorized();
            ResponseDTO emp = await _EmployeeService.GetEmps();
            if (emp.IsSuccess == false)
                return BadRequest(emp);
            return Ok(emp);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ResponseDTO>> GetEmployeeById(int id)
        {
            if (User.GetRole() == 0) return Unauthorized();

            //var Roles=User.Claims.Where(c => (c.Type == "Role")).Select(c => int.Parse(c.Value)).ToList ();
            //bool rangeExists = Roles.Any(n => n >= 5 && n <= 6);

            ResponseDTO emp = await _EmployeeService.GetEmpById(id);
            if (emp.IsSuccess == false)
                return BadRequest(emp);
            return Ok(emp);
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<ResponseDTO>> UpdateEmployee(EmployeeDto update)
        {
            ResponseDTO emp = await _EmployeeService.UpdateEmp(update);
            if (emp.IsSuccess == false)
                return BadRequest(emp);
            return Ok(emp);
        }


        [HttpGet("filterByName")]
        [Authorize]
        public async Task<ActionResult<ResponseDTO>> GetEmployeeByName(string name = "")
        {
            ResponseDTO emp = await _EmployeeService.GetEmpByName(name); ;
            if (emp.IsSuccess == false)
                return BadRequest(emp);

            return Ok(emp);
        }
        [HttpGet("filterByPhone")]
        [Authorize]
        public async Task<ActionResult<ResponseDTO>> GetEmpByPhone(string name = "")
        {
            ResponseDTO emp = await _EmployeeService.GetEmpByPhone(name);
            if (emp.IsSuccess == false)
                return BadRequest(emp);
            return Ok(emp);
        }


        [HttpPost("item")]
        [Authorize]
        public async Task<ActionResult<ResponseDTO>> UpdateItem(ItemDto2 item)
        {
            ResponseDTO emp = await _EmployeeService.UpdateItem(item);
            if (emp.IsSuccess == false)
                return BadRequest(emp);
            return Ok(emp);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<ResponseDTO>> RemoveEmp(int id)
        {
            if (id == 1) return Unauthorized();
            if (User.GetRole() != 1) return Unauthorized();
            ResponseDTO emp = await _EmployeeService.RemoveEmp(id);
            if (emp.IsSuccess == false)
                return BadRequest(emp);
            return Ok(emp);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<ResponseDTO>> RenewEmp(int id)
        {
            if (id == 1) return Unauthorized();
            if (User.GetRole() != 1) return Unauthorized();
            ResponseDTO emp = await _EmployeeService.RenewEmp(id);
            if (emp.IsSuccess == false)
                return BadRequest(emp);
            return Ok(emp);
        }
    }
}