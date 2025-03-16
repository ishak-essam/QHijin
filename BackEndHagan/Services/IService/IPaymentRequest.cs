using BackEndHagan.Models.Dto;
using QHijin.Models.Dto;

namespace QHijin.Services.IService
{
    public interface IPaymentRequestService
    {
        Task<ResponseDTO> GetPayments();
        Task<ResponseDTO> GetPaymentById(int Paymentid);
        Task<ResponseDTO> RemovePayment(int id);
        Task<ResponseDTO> UpdatePayment(PaymentRequestDto PaymentDto);
        Task<ResponseDTO> AddPaymentSend(PaymentRequestDto PaymentDto);
        Task<ResponseDTO> PaymentRecive(string RequestRef);
    }
}
