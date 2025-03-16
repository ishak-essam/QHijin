using BackEndHagan.Entities;
using BackEndHagan.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BackEndHagan.Services.IService
{
    public interface IAdminService
    {
        Task<ResponseDTO> GetAdmins ( );
        Task<ResponseDTO> GetAdminById ( int Adminid );
        Task<ResponseDTO> RemoveAdmin ( int id );
        Task<ResponseDTO> UpdateAdmin (  AdminDto admin );
        Task<ResponseDTO> AddAdmin (  AdminDto admin );
    }
}
