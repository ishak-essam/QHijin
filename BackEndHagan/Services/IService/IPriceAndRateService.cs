using BackEndHagan.Entities;
using BackEndHagan.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BackEndHagan.Services.IService
{
    public interface IPriceAndRateService
    {
        Task<ResponseDTO> GetPriceAndRate ( );
        Task<ResponseDTO> GetPriceAndRateById ( int PriceAndRateServiceid );
        Task<ResponseDTO> AddPriceAndRate ([FromBody] PriceAndRateDto Add );
        Task<ResponseDTO> UpdatePriceAndRate ( [FromBody] PriceAndRateDto PriceAndRateServiceDto );
        Task<ResponseDTO> RemovePriceAndRate( int id ); 
    }
}
