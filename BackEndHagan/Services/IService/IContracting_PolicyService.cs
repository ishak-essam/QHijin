using BackEndHagan.Models.Dto;
using QHijin.Models.Dto;

namespace QHijin.Services.IService
{
    public interface IContracting_PolicyService
    {
        Task<ResponseDTO> GetContracting_Policys();
        Task<ResponseDTO> AddContracting_Policy(Contracting_PolicyDto Add);
        Task<ResponseDTO> UpdateContracting_Policy(Contracting_PolicyDto Contracting_PolicyDto);
        Task<ResponseDTO> RemoveContracting_Policy(int id);
    }
}
