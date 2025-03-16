using AutoMapper;
using BackEndHagan.Entities;
using BackEndHagan.Models;
using BackEndHagan.Models.Dto;
using BackEndHagan.Services.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BackEndHagan.Services.Service
{
    public class AuthService : IAuthService
    {
        private readonly HaganContext _haganContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private  IMapper _mapper;
        public AuthService ( HaganContext haganContext, UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager ,IJwtTokenGenerator jwtTokenGenerator,
                IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
            _jwtTokenGenerator = jwtTokenGenerator;
            _userManager = userManager;
            _haganContext = haganContext;
            
        }
     

        public async Task<LoginResponseDTO> Login ( LoginRequestDTO requestDTO )
        {
            var user = await _userManager.FindByEmailAsync (requestDTO.Email);
            bool isVaild=await _userManager.CheckPasswordAsync(user , requestDTO.Password);

            if ( user == null || isVaild == false )
            {
                return new LoginResponseDTO () { User = null, Token = "" };
            }
            var userLogin = _haganContext.Users.Include(e=>e.Actions).ThenInclude(q=>q.Type).FirstOrDefault(u=>u.Email.ToLower() == user.Email.ToLower());
            var token= _jwtTokenGenerator.GenerateTokenUser (user,userLogin.Id);
            LoginResponseDTO loginRequestDTO=new LoginResponseDTO(){ 
                User=userLogin,
                Token=token
            };
            return loginRequestDTO;
        }

        public async Task<string> Register ( UserDTO2 requestDTO )
        {
            ApplicationUser user=new()
            {
                UserName =requestDTO.FullName.Replace(" ", ""),
                FullName =requestDTO.FullName,
                PassportNumber =requestDTO.PassportNumber,
                Email =requestDTO.Email,
                NormalizedEmail =requestDTO.Email.ToUpper(),
                PhoneNumber=requestDTO.Phone
            };
            User user1=_mapper.Map<User>(requestDTO);
            try{
                var result=await _userManager.CreateAsync(user,requestDTO.Password);
                if ( result.Succeeded )
                {
                    var createdUser = await _userManager.FindByNameAsync(user.UserName);
                    user1.Password = createdUser.PasswordHash;
                    _haganContext.Users.Add (user1);
                    _haganContext.SaveChanges ();
                    var userType = _haganContext.Users.FirstOrDefault(e=>e.FullName==requestDTO.FullName);
                    for ( var i = 0; i < requestDTO.TypeId.Count; i++ )
                    {

                        _haganContext.Actions.Update (new QHijin.Entities.Action
                        {
                            Id = 0,
                            TypeId = requestDTO.TypeId [ i ],
                            UserId = userType.Id
                        });
                        _haganContext.SaveChanges ();
                    }
                }
                else return result.Errors.FirstOrDefault ().Description;
            }
            catch(Exception ex ) {
            return ex.Message.ToString();
            }
            return "";
        }
    }
}
