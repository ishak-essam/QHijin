using BackEndHagan.Models.Dto;
using QHijin.Models.Dto;

namespace QHijin.Services.IService
{
    public interface IDelivery_PeriodService
    {
        Task<ResponseDTO> GetDelivery_Periods();
        Task<ResponseDTO> AddDelivery_Period(Delivery_PeriodDto Add);
        Task<ResponseDTO> UpdateDelivery_Period(Delivery_PeriodDto Delivery_PeriodDto);
        Task<ResponseDTO> RemoveDelivery_Period(int id);
    }
}
