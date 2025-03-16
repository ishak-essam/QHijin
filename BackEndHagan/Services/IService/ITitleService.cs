using BackEndHagan.Entities;
using BackEndHagan.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BackEndHagan.Services.IService
{
    public interface ITitleService
    {
        Task<ResponseDTO> GetTitles ( );
        Task<ResponseDTO> GetTitleById ( int Titleid );
        Task<ResponseDTO> GetTitleByEmp (  );
        Task<ResponseDTO> GetTitleByAdmin (  );
        Task<ResponseDTO> UpdateTitle ( [FromBody] Title TitleDto );
        Task<ResponseDTO> AddTitle ( [FromBody] Title Add );
        Task<ResponseDTO> RemoveTitle ( int id ); 
    }
}
