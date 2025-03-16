using AutoMapper;
using BackEndHagan.Entities;
using BackEndHagan.Extensions;
using BackEndHagan.Models.Dto;
using BackEndHagan.Services.IService;
using BackEndHagan.Services.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QHijin.Entities;
using QHijin.Models.Dto;
using QHijin.Services.IService;

namespace QHijin.Controllers
{
    [Route("api/trainer")]
    [ApiController]
    public class TrainerController : ControllerBase
    {
        private HaganContext _haganContext;
        private ResponseDTO _responseDTO;
        private ITrainerService _TrainerService;
        private IMapper _mapper;
        public TrainerController(HaganContext haganContext, IMapper mapper, ITrainerService TrainerService)
        {
            _TrainerService = TrainerService;
            _haganContext = haganContext;
            _mapper = mapper;
            _responseDTO = new ResponseDTO();
        }
        [HttpPost]
        public async Task<ActionResult<ResponseDTO>> AddTrainer(trainerDto Add)
        {
        
            try
            {
                var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                Add.trnImgLocalPath = baseUrl;
                _responseDTO=await _TrainerService.AddTrainer(Add);    
                if (_responseDTO.IsSuccess==false) {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                _responseDTO.Message = ex.Message.ToString();
                _responseDTO.IsSuccess = false;
            }
            if (_responseDTO.IsSuccess == false)
                return BadRequest(_responseDTO);
            return Ok(_responseDTO);
        }


        [HttpPut]
        public async Task<ActionResult<ResponseDTO>> UpdateTrainer(trainerDto Add)
        {
            try
            {

                var item =await _haganContext.Trainer.FirstOrDefaultAsync(u => u.trnId == Add.trnId);
                if (item != null)
                {

                    var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                    Add.trnImgLocalPath = baseUrl;
                    _responseDTO = await _TrainerService.UpdateTrainer(Add);
                }
                else
                {
                    return BadRequest("trainer isn't exist ");
                }

            }
            catch (Exception ex)
            {
                _responseDTO.Message = ex.Message.ToString();
                _responseDTO.IsSuccess = false;
            }
            if (_responseDTO.IsSuccess == false)
                return BadRequest(_responseDTO);
            return Ok(_responseDTO);
        }
        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> GetUsers()
        {
            ResponseDTO emp = await _TrainerService.GetTrainer();
            if (emp.IsSuccess == false)
                return BadRequest(emp);
            return Ok(emp);
        }
        [HttpDelete("{id}")]
        [Authorize]

        public async Task<ActionResult<ResponseDTO>> RemoveBanner(int id)
        {
            ResponseDTO banner = await _TrainerService.RemoveTrainer(id);
            if (banner.IsSuccess == false)
                return BadRequest(banner);
            return Ok(banner);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDTO>> GetBannerById(int id)
        {
            ResponseDTO emp = await _TrainerService.GetTrainerById(id);
            if (emp.IsSuccess == false)
                return BadRequest(emp);
            return Ok(emp);
        }

    }
}
