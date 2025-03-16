using BackEndHagan.Models.Dto;
using QHijin.Models.Dto;

namespace QHijin.Services.IService
{
    public interface IHowTobuyService
    {
        Task<ResponseDTO> GetHowTobuys();
        Task<ResponseDTO> AddHowTobuy(HowTobuyDto Add);
        Task<ResponseDTO> UpdateHowTobuy(HowTobuyDto HowTobuyDto);
        Task<ResponseDTO> RemoveHowTobuy(int id);
    }
}
