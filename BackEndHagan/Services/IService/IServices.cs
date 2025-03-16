using BackEndHagan.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using QHijin.Models.Dto;

namespace QHijin.Services.IService
{
    public interface IServices
    {
        Task<ResponseDTO> GetService();
        Task<ResponseDTO> GetServiceById(int Serviceid);
        Task<ResponseDTO> UpdateService( ServiceDto ServiceDto);
        Task<ResponseDTO> AddService( ServiceDto Add);
        Task<ResponseDTO> RemoveService(int id);
    }
}
