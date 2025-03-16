using BackEndHagan.Entities;
using BackEndHagan.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BackEndHagan.Services.IService
{
    public interface IUserService
    {
        Task<ResponseDTO> GetUsers ( );
        Task<ResponseDTO> GetUserById ( int Userid );
        Task<ResponseDTO> GetUserByName ( string name = "" );
        Task<ResponseDTO> GetUserByPhone ( string phone = "" );
        Task<ResponseDTO> UpdateUser (  UserDTO2 UserDto );
        Task<ResponseDTO> RemoveUser (int id ); 
        Task<ResponseDTO> RenewUser (int id ); 
    }
}
