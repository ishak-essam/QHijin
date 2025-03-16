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
    public class FeedbackService : IFeedbackService
    {
        private IMapper _mapper;

        private HaganContext _haganContext;
        private ResponseDTO _responseDTO;
        public FeedbackService(IMapper mapper, HaganContext haganContext
            )
        {
            _haganContext = haganContext;
            _mapper = mapper;
            _responseDTO = new ResponseDTO();
        }
        public async Task<ResponseDTO> AddFeedback( FeedbackDto Add)
        {
            try
            {
                await _haganContext.Feedback.AddAsync(_mapper.Map<Feedback>(Add));
              await  _haganContext.SaveChangesAsync();
                _responseDTO.Result = "Created";
            }
            catch (Exception ex)
            {
                _responseDTO.Message = ex.Message.ToString();
                _responseDTO.IsSuccess = false;
            }
            return _responseDTO;
        }
        public async Task<ResponseDTO> GetFeedbackById(int FeedbackNo)
        {
            try
            {
                var P_P = await  _haganContext.Feedback.Include(e => e.User).FirstOrDefaultAsync(ele => ele.FBId == FeedbackNo);
                if (P_P != null)
                {
                    _responseDTO.Result = P_P;
                }
                else
                {
                    _responseDTO.Message = "Feedback isn't exists";
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



        public async Task<ResponseDTO> GetFeedbacks()
        {
            try
            {
                var P_Ps = await _haganContext.Feedback.Include(e=>e.User).ToListAsync();
                if (P_Ps != null)
                {
                    _responseDTO.Result = P_Ps;
                }
                else
                {
                    _responseDTO.Message = "Feedback isn't exists";
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

        public async Task<ResponseDTO> RemoveFeedback(int id)
        {
            try
            {
                var P_P = await _haganContext.Feedback.FirstOrDefaultAsync(ele => ele.FBId == id);
                if (P_P != null)
                {
                    _haganContext.Feedback.Remove(P_P);
                    await _haganContext.SaveChangesAsync();
                    _responseDTO.Result = "Deleted";
                }
                else
                {
                    _responseDTO.Message = "Feedback isn't exists";
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


        public async Task<ResponseDTO> UpdateFeedback( FeedbackDto FeedbackDto)
        {
            try
            {
                var Feedback = await _haganContext.Feedback.FirstOrDefaultAsync(ele => ele.FBId == FeedbackDto.FBId);
                if (Feedback != null)
                {
                    Feedback.email = FeedbackDto.email;
                    Feedback.text = FeedbackDto.text;
                    Feedback.UserId = FeedbackDto.UserId;
                    _haganContext.Feedback.Update(Feedback);
                    _haganContext.SaveChanges();
                    _responseDTO.Result = "Updated";
                }
                else
                {
                    _responseDTO.Message = "Feedback isn't exists";
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
    }
}