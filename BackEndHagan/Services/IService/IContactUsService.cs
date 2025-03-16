using BackEndHagan.Models.Dto;
using QHijin.Models.Dto;

namespace QHijin.ContactUs.IContactUs
{
    public interface IContactUsService
    {
        Task<ResponseDTO> GetContactUs();
        Task<ResponseDTO> GetContactUsById(int ContactUsid);
        Task<ResponseDTO> UpdateContactUs(ContactUsDto ContactUsDto);
        Task<ResponseDTO> AddContactUs(ContactUsDto Add);
        Task<ResponseDTO> RemoveContactUs(int id);
    }
}
