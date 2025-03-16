using BackEndHagan.Entities;
using BackEndHagan.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BackEndHagan.Services.IService
{
    public interface ITermsAndConditionService
    {

        Task<ResponseDTO> GetTermsAndConditions ( );
        Task<ResponseDTO> UpdateTermsAndCondition (  TermsAndCondition TermsAndConditionDto );
        Task<ResponseDTO> AddTermsAndCondition ( [FromBody] TermsAndConditionDto Add );
        Task<ResponseDTO> RemoveTermsAndCondition ( int id );
    }
}
