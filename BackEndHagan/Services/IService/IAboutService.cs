using BackEndHagan.Entities;
using BackEndHagan.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BackEndHagan.Services.IService
{
    public interface IAboutService
    {
        Task<ResponseDTO> GetAbouts ( );
        Task<ResponseDTO> GetAboutById ( int Aboutid );
        Task<ResponseDTO> UpdateAbout ( [FromBody] AboutDto AboutDto );
        Task<ResponseDTO> AddAbout ( [FromBody] AboutDto Add );
        Task<ResponseDTO> RemoveAbout ( int id ); 
        Task<ResponseDTO> RemovePhotoAbout( int id );
    }
}
