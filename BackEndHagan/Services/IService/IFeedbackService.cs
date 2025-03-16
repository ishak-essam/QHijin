using BackEndHagan.Entities;
using BackEndHagan.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using QHijin.Models.Dto;

namespace QHijin.Services.IService
{
    public interface IFeedbackService
    {
        Task<ResponseDTO> GetFeedbacks();
        Task<ResponseDTO> GetFeedbackById(int Feedbackid);
        Task<ResponseDTO> UpdateFeedback( FeedbackDto FeedbackDto);
        Task<ResponseDTO> AddFeedback( FeedbackDto Add);
        Task<ResponseDTO> RemoveFeedback(int id);
    }
}
