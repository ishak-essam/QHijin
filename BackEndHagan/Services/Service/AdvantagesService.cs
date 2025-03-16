using AutoMapper;
using BackEndHagan.Entities;
using BackEndHagan.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QHijin.Entities;
using QHijin.Models.Dto;
using QHijin.Services.IService;

namespace QHijin.Services.Service
{
    public class AdvantagesService : IAdvantages
    {
        private HaganContext _haganContext;
        private ResponseDTO _responseDTO;
        private IMapper _mapper;
        public AdvantagesService(HaganContext haganContext, IMapper mapper)
        {
            _haganContext = haganContext;
            _mapper = mapper;
            _responseDTO = new ResponseDTO();
        }
        public async Task<ResponseDTO> GetAdvantageById(int AdvId)
        {
            try
            {
                var Advantage = await _haganContext.Advantages.FirstOrDefaultAsync(x => x.AdvId == AdvId);
                if (Advantage != null)
                {
                    _responseDTO.Result = Advantage;
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


        public async Task<ResponseDTO> GetAdvantage()
        {
            try
            {
                var items = await _haganContext.Advantages.ToListAsync();
                _responseDTO.Result = items;
            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message.ToString();
            }
            return _responseDTO;
        }
        public async Task<ResponseDTO> RemoveAdvantage(int AdvId)
        {
            try
            {
                var Advantage = await _haganContext.Advantages.FirstOrDefaultAsync(u => u.AdvId == AdvId);
                if (Advantage != null)
                {
                    _haganContext.Advantages.Remove(Advantage);
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

        public async Task<ResponseDTO> UpdateAdvantage(AdvantageDto AdvantageDto)
        {
            try
            {
                var item = _mapper.Map<Advantages>(AdvantageDto);
                var Advantage = await _haganContext.Advantages.FirstOrDefaultAsync(u => u.AdvId == item.AdvId);
                if (Advantage != null)
                {
                    Advantage.TitleEn = item.TitleEn;
                    Advantage.TitleAr = item.TitleAr; 
                    Advantage.TextEn = item.TextEn;
                    Advantage.TextAr = item.TextAr;
                    Advantage.EmpId = item.EmpId;
                    _haganContext.Advantages.Update(Advantage);
                    _haganContext.SaveChanges();
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
        public async Task<ResponseDTO> AddAdvantage([FromBody] AdvantageDto AdvantageDto)
        {
            try
            {
                await _haganContext.Advantages.AddAsync(_mapper.Map<Advantages>(AdvantageDto));
                await _haganContext.SaveChangesAsync();
                _responseDTO.Result = "Advantage Created";
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
