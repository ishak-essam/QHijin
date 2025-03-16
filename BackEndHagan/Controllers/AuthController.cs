using AutoMapper;
using BackEndHagan.Entities;
using BackEndHagan.Models.Dto;
using BackEndHagan.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace BackEndHagan.Controllers
{
    [ApiController]
    [Route ("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ResponseDTO _responseDTO;
        private  IMapper _mapper;
        private  HaganContext _haganContext;

        public AuthController ( HaganContext haganContext, IAuthService authService, IMapper mapper )
        {
           _haganContext = haganContext;
            _authService = authService;
            _mapper = mapper;
            _responseDTO = new ();
        }

        [HttpPost ("register")]
        public async Task<IActionResult> Register (  UserDTO2 registrationRequestDTO )
        {

            User user= _mapper.Map<User>(registrationRequestDTO);
            user.Invoices = [ ];
            user.Biddings = [ ];
            user.Items = [ ];
            user.Contacts = [ ];
            var titles = _haganContext.Types.ToList();
            bool allInList = registrationRequestDTO.TypeId.All(x => titles.Any(ele => ele.TypeId == x));
            if ( allInList == false )
            {
                _responseDTO.Message = "Type isn't exists";
                _responseDTO.IsSuccess = false;
                return BadRequest (_responseDTO);
            }
            var errorMessage=await _authService.Register(registrationRequestDTO);
            if ( !string.IsNullOrEmpty (errorMessage) )
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = errorMessage;
                return BadRequest (_responseDTO);
            }
            _responseDTO.Message = "Account Created";
            return Ok (_responseDTO);
        }
        [HttpPost ("login")]
        public async Task<IActionResult> Login ([FromBody]LoginRequestDTO loginRequestDTO )
        {
            var loginResponse=await _authService.Login(loginRequestDTO);
            if ( loginResponse.User == null ) {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = "email or password isn't correct";
                return BadRequest (_responseDTO);   
            }
            _responseDTO.Result = loginResponse;
            return Ok (_responseDTO);
        }
    }
}
