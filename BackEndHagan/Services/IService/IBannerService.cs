using BackEndHagan.Entities;
using BackEndHagan.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BackEndHagan.Services.IService
{
    public interface IBannerService
    {
        Task<ResponseDTO> GetBanners ( );
        Task<ResponseDTO> GetBannerById ( int Bannerid );
        Task<ResponseDTO> GetBannerByEmpId ( int id );
        Task<ResponseDTO> UpdateBanner ( BannerDto BannerDto );
        Task<ResponseDTO> AddBanner ( BannerDto Add );
        Task<ResponseDTO> RemoveBanner ( int id );
    }
}
