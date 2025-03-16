using AutoMapper;
using BackEndHagan.Entities;
using BackEndHagan.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QHijin.ContactUs.IContactUs;
using QHijin.Entities;
using QHijin.Models.Dto;

namespace QHijin.Services.ContactUs
{
    public class ContactUsService : IContactUsService
    {
        private HaganContext _haganContext;
        private ResponseDTO _responseDTO;
        private IMapper _mapper;
        public ContactUsService(HaganContext haganContext, IMapper mapper)
        {
            _haganContext = haganContext;
            _mapper = mapper;
            _responseDTO = new ResponseDTO();
        }
        public async Task<ResponseDTO> GetContactUsById(int ContId)
        {
            try
            {
                var ContactUs =await _haganContext.ContactUs.FirstOrDefaultAsync(x => x.ContId == ContId);
                if (ContactUs != null)
                {
                    _responseDTO.Result = ContactUs;
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


        public async Task<ResponseDTO> GetContactUs()
        {
            try
            {
                var items =await _haganContext.ContactUs.ToListAsync();
                _responseDTO.Result = items;
            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message.ToString();
            }
            return _responseDTO;
        }
        public async Task<ResponseDTO> RemoveContactUs(int ContId)
        {
            try
            {
                var ContactUs =await _haganContext.ContactUs.FirstOrDefaultAsync(u => u.ContId == ContId);
                if (ContactUs != null)
                {
                    _haganContext.ContactUs.Remove(ContactUs);
                  await  _haganContext.SaveChangesAsync();
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

        public async Task<ResponseDTO> UpdateContactUs( ContactUsDto ContactUsDto)
        {
            try
            {
                var item = _mapper.Map<QHijin.Entities.ContactUs>(ContactUsDto);
                var ContactUs =await _haganContext.ContactUs.FirstOrDefaultAsync(u => u.ContId == item.ContId);
                if (ContactUs != null)
                {
                    ContactUs.TextEn = item.TextEn;
                    ContactUs.TextAr = item.TextAr;
                    ContactUs.EmpId = item.EmpId;
                    _haganContext.ContactUs.Update(ContactUs);
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
        public async Task<ResponseDTO> AddContactUs([FromBody] ContactUsDto ContactUsDto)
        {
            try
            {
                await _haganContext.ContactUs.AddAsync(_mapper.Map<QHijin.Entities.ContactUs>(ContactUsDto));
               await _haganContext.SaveChangesAsync();
                _responseDTO.Result = "ContactUs Created";
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
