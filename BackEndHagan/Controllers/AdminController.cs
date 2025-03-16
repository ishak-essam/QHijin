using AutoMapper;
using BackEndHagan.Entities;
using BackEndHagan.Extensions;
using BackEndHagan.Models.Dto;
using BackEndHagan.Services.IService;
using BackEndHagan.Services.Service;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace BackEndHagan.Controllers
{
    [ApiController]
    [Route ("api/admin")]
    public class AdminController : ControllerBase
    {
        private IAdminService _AdminService ;
        private HaganContext _haganContext;
        private  IJwtTokenGenerator _jwtTokenGenerator;
        private  IMapper _mapper;
        public AdminController (HaganContext haganContext, IAdminService AdminService,
            IMapper mapper, IJwtTokenGenerator jwtTokenGenerator )
        {
            _haganContext = haganContext;
            _mapper = mapper;
            _jwtTokenGenerator = jwtTokenGenerator;
            _AdminService = AdminService;
        }


        [HttpPost ("login")]
        public async Task<ActionResult<ResponseDTO>> Login ( AdminLoginRequestDTO adminLogin )
        {
            var Admin=await _haganContext.Admins.FirstOrDefaultAsync(u=>u.EmpId==adminLogin.id);
            var admin=true;
            if ( Admin == null ) return BadRequest ("invalid id");
            using var hmac=new HMACSHA512(Admin.PasswordSalt);
            var ComputedHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(adminLogin.Password));
            for ( int i = 0; i < ComputedHash.Length; i++ )
            {
                if ( ComputedHash [ i ] != Admin.PasswordHash [ i ] )
                    return Unauthorized ("Invalid password");
            }

            if ( Admin != null ) admin = true;
            else admin = false;
            var employee=await _haganContext.Employees.FirstOrDefaultAsync(u=>u.EmpId==adminLogin.id); 
            var token= _jwtTokenGenerator.GenerateTokenDashboard (employee!,admin,Admin!.TitleId.ToString
                ());
            LoginResponseDTO loginRequestDTO=new LoginResponseDTO
            {
                Employee=_mapper.Map<EmployeeDto>(employee),
                Token=token
            };
            return Ok (loginRequestDTO);
        }


        [HttpGet]
        [Authorize]
       public async Task<ActionResult<ResponseDTO>>  GetAdmins ( )
        {
            if ( User.GetRole() != 1  ) return Unauthorized ("Only Admin has authorized");
            var IsAdmin=User.GetAdminRole();
            if ( IsAdmin == "False" || IsAdmin == null ) return Unauthorized ("Only Admin has authorized");
            ResponseDTO  emp=   await _AdminService.GetAdmins ();
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }

        [HttpGet ("{id}")]
        [Authorize]

        public async Task<ActionResult<ResponseDTO>>  GetAdminById ( int id )
        {
            if (User.GetRole() != 1 ) return Unauthorized("Only Admin has authorized");
            var IsAdmin=User.GetAdminRole();
            if ( IsAdmin == "False" || IsAdmin == null ) return Unauthorized ("Only Admin has authorized");
            return Ok (await _AdminService.GetAdminById (id));
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ResponseDTO>>  AddAdmin (  AdminDto Admin )
        {
            if (User.GetRole() == 1 ) return Unauthorized("Only Admin has authorized");
            var IsAdmin=User.GetAdminRole();
            if ( IsAdmin == "False" || IsAdmin == null ) return Unauthorized ("Only Admin has authorized");
            return Ok (await _AdminService.AddAdmin (Admin));
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<ResponseDTO>> UpdateAdmin ( AdminDto Admin )
        {
            if (User.GetRole() != 1) return Unauthorized("Only Admin has authorized");
            var IsAdmin=User.GetAdminRole();
            if ( IsAdmin == "False" || IsAdmin == null ) return Unauthorized ("Only Admin has authorized");
            return Ok (await _AdminService.UpdateAdmin (Admin));
        }
        [HttpDelete]
        [Authorize]
        public async Task<ActionResult<ResponseDTO>> RemoveAdmin ( int id )
        {
            if (User.GetRole() != 1 ) return Unauthorized("Only Admin has authorized");
            if ( id == 1 ) return Unauthorized ("Owner can't delete");
            var IsAdmin=User.GetAdminRole();
            if ( IsAdmin == "False" || IsAdmin == null ) return Unauthorized ("Only Admin has authorized");
            return Ok (await _AdminService.RemoveAdmin (id));
        }
    }
}