using AutoMapper;
using BackEndHagan.Entities;
using BackEndHagan.Extensions;
using BackEndHagan.Models.Dto;
using BackEndHagan.Services.IService;
using BackEndHagan.Services.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QHijin.Models;
using QHijin.Services.IService;

namespace QHijin.Controllers
{
    [Route("api/ads")]
    [ApiController]
    public class AdsController : ControllerBase
    {
        private HaganContext _haganContext;
        private ResponseDTO _responseDTO;
        private IAdsService _AdsService;
        private IMapper _mapper;
        public AdsController(HaganContext haganContext, IMapper mapper, IAdsService AdsService)
        {
            _AdsService = AdsService;
            _haganContext = haganContext;
            _mapper = mapper;
            _responseDTO = new ResponseDTO();
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> GetAds()
        {
            if (User.GetRole()==0) return Unauthorized();
            ResponseDTO emp = await _AdsService.GetAds();
            if (emp.IsSuccess == false)
                return BadRequest(emp);
            return Ok(emp);
        }


        [HttpPost]
        public async Task<ActionResult<ResponseDTO>> AddAds(AdsDto adsDto)
        {
            if (User.GetRole()==0) return Unauthorized();
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
            adsDto.AdsImgUrl = baseUrl;
            try
            {
                if (adsDto.Image != null)
                {
                    ResponseDTO emp = await _AdsService.AddAds(adsDto);
                    if (_responseDTO.IsSuccess == false)
                        return BadRequest(_responseDTO);
                    return Ok(emp);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                _responseDTO.Message = ex.Message.ToString();
                _responseDTO.IsSuccess = false;
            }

            return Ok(_responseDTO);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDTO>> GetAdsById(int id)
        {
            if (User.GetRole()==0) return Unauthorized();
            ResponseDTO emp = await _AdsService.GetAdsById(id);
            if (emp.IsSuccess == false)
                return BadRequest(emp);
            return Ok(emp);
        }


        [HttpGet("filterBytype")]
        public async Task<ActionResult<ResponseDTO>> GetAdsByName(string type = "")
        {
            ResponseDTO emp = await _AdsService.GetAdsByType(type);
            if (emp.IsSuccess == false)
                return BadRequest(emp);
            return Ok(emp);
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult<ResponseDTO>> RemoveAds(int id)
        {
            if (User.GetRole()==0) return Unauthorized();
            ResponseDTO banner = await _AdsService.RemoveAds(id);
            if (banner.IsSuccess == false)
                return BadRequest(banner);
            return Ok(banner);
        }


        [HttpPut]
        [Authorize]
        public async Task<ActionResult<ResponseDTO>> UpdateAdsById(AdsDto update)
        {
            if (User.GetRole()==0) return Unauthorized();
            update.AdsImgUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
            ResponseDTO responseDTO = await _AdsService.UpdateAds(update);
            if (responseDTO.IsSuccess == false)
                return BadRequest(responseDTO);
            return Ok(responseDTO);
        }
    }
}
