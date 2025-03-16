using BackEndHagan.Entities;
using BackEndHagan.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BackEndHagan.Services.IService
{
    public interface ITypeService
    {
        Task<ResponseDTO> GetTypes ( );
        Task<ResponseDTO> GetTypeById ( int Typeid );
        Task<ResponseDTO> GetTypeByName ( string name = "" );
        Task<ResponseDTO> UpdateType ( [FromBody] TypeDto TypeDto );
        Task<ResponseDTO> AddType ( [FromBody] TypeDto Add );
        Task<ResponseDTO> RemoveType ( int id ); 
        Task<ResponseDTO> ReNewType ( int id ); 
    }
}
