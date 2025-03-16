using BackEndHagan.Models.Dto;
using QHijin.Models.Dto;

namespace QHijin.Services.IService
{
    public interface ITrainerService
    {
        Task<ResponseDTO> GetTrainer();
        Task<ResponseDTO> GetTrainerById(int trneid);
        Task<ResponseDTO> UpdateTrainer(trainerDto trainerDto);
        Task<ResponseDTO> AddTrainer(trainerDto trainerDto);
        Task<ResponseDTO> RemoveTrainer(int id);
    }
}
