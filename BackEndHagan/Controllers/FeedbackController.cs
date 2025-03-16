using BackEndHagan.Entities;
using BackEndHagan.Extensions;
using BackEndHagan.Models.Dto;
using BackEndHagan.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QHijin.Models.Dto;
using QHijin.Services.IService;

namespace QHijin.Controllers
{
    [Route("api/feedback")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private IFeedbackService _FeedbackService;

        public FeedbackController(IFeedbackService FeedbackService)
        {
            _FeedbackService = FeedbackService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> GetFeedbacks()
        {
            ResponseDTO emp = await _FeedbackService.GetFeedbacks();
            if (emp.IsSuccess == false)
                return BadRequest(emp);
            return Ok(emp);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDTO>> GetFeedbackById(int id)
        {
            ResponseDTO emp = await _FeedbackService.GetFeedbackById(id);
            if (emp.IsSuccess == false)
                return BadRequest(emp);
            return Ok(emp);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDTO>> AddFeedback( FeedbackDto Feedback)
        {
            ResponseDTO emp = await _FeedbackService.AddFeedback(Feedback);
            if (emp.IsSuccess == false)
                return BadRequest(emp);
            return Ok(emp);
        }

        
        [HttpPut]
        [Authorize]
        public async Task<ActionResult<ResponseDTO>> UpdateFeedback( FeedbackDto Feedback)
        {
            if (!User.GetRoles().Contains(1) && !User.GetRoles().Contains(2)) return Unauthorized("Only Admin has authorized");

            ResponseDTO emp = await _FeedbackService.UpdateFeedback(Feedback);
            if (emp.IsSuccess == false)
                return BadRequest(emp);
            return Ok(emp);
        }
        [HttpDelete]
        [Authorize]

        public async Task<ActionResult<ResponseDTO>> RemoveFeedback(int id)
        {
            if (!User.GetRoles().Contains(1) && !User.GetRoles().Contains(2)) return Unauthorized("Only Admin has authorized");

            ResponseDTO emp = await _FeedbackService.RemoveFeedback(id);
            if (emp.IsSuccess == false)
                return BadRequest(emp);
            return Ok(emp);
        }
    }
}
