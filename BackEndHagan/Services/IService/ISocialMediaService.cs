using BackEndHagan.Entities;
using BackEndHagan.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BackEndHagan.Services.IService
{
    public interface ISocialMediaService
    {
        Task<ResponseDTO> GetSocialMedias ( );
        Task<ResponseDTO> GetSocialMediaById ( int SocialMediaid );
        Task<ResponseDTO> GetSocialMediaByName ( string name = "" );
        Task<ResponseDTO> UpdateSocialMedia (  SocialMediaDto SocialMediaDto );
        Task<ResponseDTO> AddSocialMedia (  SocialMediaDto Add );
        Task<ResponseDTO> RemoveSocialMedia ( int id );
    }
}
