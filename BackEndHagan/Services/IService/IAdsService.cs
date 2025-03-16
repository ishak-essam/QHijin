using BackEndHagan.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using QHijin.Models;

namespace QHijin.Services.IService
{
    public interface IAdsService
    {
        Task<ResponseDTO> GetAds();
        Task<ResponseDTO> GetAdsById(int Adsid);
        Task<ResponseDTO> GetAdsByType(string name = "");
        Task<ResponseDTO> UpdateAds( AdsDto AdsDto);
        Task<ResponseDTO> AddAds(AdsDto Add);
        Task<ResponseDTO> RemoveAds(int id);
    }
}
