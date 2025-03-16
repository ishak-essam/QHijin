using BackEndHagan.Entities;
using BackEndHagan.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BackEndHagan.Services.IService
{
    public interface IContactService
    {
        Task<ResponseDTO> GetContacts ( );
        Task<ResponseDTO> GetContactById ( int Contactid );
        Task<ResponseDTO> GetContactByUser ( int id  );
        Task<ResponseDTO> UpdateContact ( [FromBody] ContactDto ContactDto );
        Task<ResponseDTO> AddContact ( [FromBody] ContactDto Add );
        Task<ResponseDTO> RemoveContact (int id );
    }
}
