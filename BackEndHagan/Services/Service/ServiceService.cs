using AutoMapper;
using BackEndHagan.Entities;
using BackEndHagan.Models.Dto;
using BackEndHagan.Services.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QHijin.Entities;
using QHijin.Models.Dto;
using QHijin.Services.IService;

namespace QHijin.Services.Service
{
    public class ServiceService : IServices
    {
        private HaganContext _haganContext;
        private ResponseDTO _responseDTO;
        private IMapper _mapper;
        public ServiceService(HaganContext haganContext, IMapper mapper)
        {
            _haganContext = haganContext;
            _mapper = mapper;
            _responseDTO = new ResponseDTO();
        }
        public async Task<ResponseDTO> GetServiceById(int SerId)
        {
            try
            {
                var Service = await _haganContext.Services.FirstOrDefaultAsync(x => x.SerId == SerId);
                if (Service != null)
                {
                    _responseDTO.Result = Service;
                }
                else
                {
                    _responseDTO.IsSuccess = false;
                    _responseDTO.Message = "It isn't exist";
                }
            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message.ToString();
            }
            return _responseDTO;
        }


        public async Task<ResponseDTO> GetService()
        {
            try
            {
                var items = await _haganContext.Services.ToListAsync();
                _responseDTO.Result = items;
            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message.ToString();
            }
            return _responseDTO;
        }
        public async Task<ResponseDTO> RemoveService(int SerId)
        {
            try
            {
                var Service = await _haganContext.Services.FirstOrDefaultAsync(u => u.SerId == SerId);
                if (Service != null)
                {
                    _haganContext.Services.Remove(Service);
                    _haganContext.SaveChanges();
                    _responseDTO.Result = "Deleted";
                }
                else
                {
                    _responseDTO.Message = "it isn't exist";
                    _responseDTO.IsSuccess = false;
                }
            }
            catch (Exception ex)
            {
                _responseDTO.Message = ex.Message.ToString();
                _responseDTO.IsSuccess = false;
            }
            return _responseDTO;
        }

        public async Task<ResponseDTO> UpdateService([FromBody] ServiceDto ServiceDto)
        {
            try
            {
                var item = _mapper.Map<ServicesSite>(ServiceDto);
                var Service = await _haganContext.Services.FirstOrDefaultAsync(u => u.SerId == item.SerId);
                if (Service != null)
                {
                    Service.SubTitleAr = item.SubTitleAr;
                    Service.ParphAr = item.ParphAr;
                    Service.ParphEn = item.ParphEn;
                    Service.EmpId = item.EmpId;
                    Service.SubTitleEn = item.SubTitleEn;
                    _haganContext.Services.Update(Service);
                    await _haganContext.SaveChangesAsync();
                    _responseDTO.Result = "Updated";
                }
                else
                {
                    _responseDTO.Message = "item isn't exists ";
                    _responseDTO.IsSuccess = false;
                }
            }
            catch (Exception ex)
            {
                _responseDTO.Message = ex.Message;
                _responseDTO.IsSuccess = false;
            }
            return _responseDTO;
        }
        public async Task<ResponseDTO> AddService([FromBody] ServiceDto ServiceDto)
        {
            try
            {
                await _haganContext.Services.AddAsync(_mapper.Map<ServicesSite>(ServiceDto));
                await _haganContext.SaveChangesAsync();
                _responseDTO.Result = "Service Created";
            }
            catch (Exception ex)
            {
                _responseDTO.Message = ex.Message;
                _responseDTO.IsSuccess = false;
            }
            return _responseDTO;
        }
    }
}
