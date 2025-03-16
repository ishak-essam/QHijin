using BackEndHagan.Models.Dto;
using QHijin.Models.Dto;

namespace QHijin.Services.IService
{
    public interface IAdvantages
    {
        Task<ResponseDTO> GetAdvantage();
        Task<ResponseDTO> GetAdvantageById(int Advantageid);
        Task<ResponseDTO> UpdateAdvantage(AdvantageDto AdvantageDto);
        Task<ResponseDTO> AddAdvantage(AdvantageDto Add);
        Task<ResponseDTO> RemoveAdvantage(int id);
    }
}
