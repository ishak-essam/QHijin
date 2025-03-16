using BackEndHagan.Entities;
using BackEndHagan.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BackEndHagan.Services.IService
{
    public interface IPrivacyAndPolicyService
    {
        Task<ResponseDTO> GetPrivacyAndPolicys ( );
        Task<ResponseDTO> AddPrivacyAndPolicy ( PrivacyAndPolicyDto Add );
        Task<ResponseDTO> UpdatePrivacyAndPolicy ( PrivacyAndPolicyDto PrivacyAndPolicyDto );
        Task<ResponseDTO> RemovePrivacyAndPolicy ( int id ); 
    }
}
