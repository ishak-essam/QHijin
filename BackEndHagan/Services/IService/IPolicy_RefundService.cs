using BackEndHagan.Models.Dto;
using QHijin.Models.Dto;

namespace QHijin.Services.IService
{
    public interface  IPolicy_RefundService
    {
        Task<ResponseDTO> GetPolicy_Refunds();
        Task<ResponseDTO> AddPolicy_Refund(Policy_RefundDto Add);
        Task<ResponseDTO> UpdatePolicy_Refund(Policy_RefundDto Policy_RefundDto);
        Task<ResponseDTO> RemovePolicy_Refund(int id);
    }
}
