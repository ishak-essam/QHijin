using BackEndHagan.Entities;
using BackEndHagan.Models.Dto;

namespace BackEndHagan.Services.IService
{
    public interface IAuthService
    {
        Task<string> Register ( UserDTO2 requestDTO );
        Task<LoginResponseDTO> Login ( LoginRequestDTO requestDTO );
    }
}
