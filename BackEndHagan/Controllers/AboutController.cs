using AutoMapper;
using BackEndHagan.Entities;
using BackEndHagan.Extensions;
using BackEndHagan.Models.Dto;
using BackEndHagan.Services.IService;
using BackEndHagan.Services.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QHijin.Entities;
using QHijin.Models.Dto;

namespace BackEndHagan.Controllers
{
    [Route("api/about")]
    [ApiController]
    public class AboutController : ControllerBase
    {
        private IMapper _mapper;
        private IAboutService _AboutService;
        private HaganContext _haganContext;

        public AboutController(HaganContext haganContext, IMapper mapper, IAboutService AboutService)
        {
            _AboutService = AboutService;
            _mapper = mapper;
            _haganContext = haganContext;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> GetAbouts()
        {
            ResponseDTO emp = await _AboutService.GetAbouts();
            if (emp.IsSuccess == false)
                return BadRequest(emp);
            return Ok(emp);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ResponseDTO>> UpdateAbout(AboutDto About)
        {
            if (User.GetRole()==0) return Unauthorized();
            ResponseDTO emp = await _AboutService.UpdateAbout(About);
            if (emp.IsSuccess == false)
                return BadRequest(emp);
            return Ok(emp);
        }

        [HttpPost("photo")]
        [Authorize]
        public async Task<ActionResult<ResponseDTO>> AddPhotoAbout(AboutPhotosDTO About)
        {
            ResponseDTO responseDTO = new ResponseDTO();

            if (User.GetRole()==0) return Unauthorized();
            try
            {
                var UserId = await _haganContext.Abouts.FirstOrDefaultAsync(ele => ele.AboutNo == 1);
                var images =  About.Images.ToList();
                for (var i = 0; i < About.Images.Count; i++)
                {
                    AboutPhotos photo = _mapper.Map<AboutPhotos>(About);
                    var lastPhoto2 = await _haganContext.AboutPhoto.ToListAsync();
                    var id = 1;
                    if (lastPhoto2.Count == 0)
                    {
                        id = 1;
                    }
                    else
                        id = lastPhoto2.Last().AbPhId + 1;
                    if (images[i] != null)
                    {
                        string extension = Path.GetExtension(images[i].FileName);
                        string fileName = id + extension;
                        string filePath = @"wwwroot/About/" + fileName;
                        var filePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                        using (var fileStream = new FileStream(filePathDirectory, FileMode.Create))
                        {
                            images[i].CopyTo(fileStream);
                        }
                        var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                        photo.ImgUrl = baseUrl + "/About/" + fileName;
                        photo.ImgLocalPath = filePath;
                        photo.AboutId = 1;
                    }
                    _haganContext.AboutPhoto.Add(photo);
                    _haganContext.SaveChanges();
                }
                responseDTO.Result = "Created";
            }
            catch (Exception ex)
            {
                responseDTO.Message = ex.Message.ToString();
                responseDTO.IsSuccess = false;
            }

     
            return Ok(responseDTO);
        }
        [HttpDelete]
        [Authorize]
        public async Task<ActionResult<ResponseDTO>> RemoveAbout(int id)
        {
            if (User.GetRole()==0) return Unauthorized();
            ResponseDTO emp = await _AboutService.RemoveAbout(id);
            if (emp.IsSuccess == false)
                return BadRequest(emp);
            return Ok(emp);
        }

        [HttpDelete("photo/{id}")]
        [Authorize]

        public async Task<ActionResult<ResponseDTO>> RemovePhotoAbout(int id)
        {
            if (User.GetRole()==0) return Unauthorized();
            ResponseDTO img = await _AboutService.RemovePhotoAbout(id);
            if (img.IsSuccess == false)
                return BadRequest(img);
            return Ok(img);
        }

        [HttpPut("photo")]
        [Authorize]

        public async Task<ActionResult<ResponseDTO>> UpdatePhotoAboutById(AboutPhotoDTO update)
        {
            if (User.GetRole()==0) return Unauthorized();
            ResponseDTO _responseDTO = new ResponseDTO();
            try
            {
                AboutPhotos product = _mapper.Map<AboutPhotos>(update);
                var Image = await _haganContext.AboutPhoto.FirstOrDefaultAsync(ele => ele.AbPhId == update.AbPhId);
                if (Image != null)
                {
                    var images = update.Image;
                    if (images != null)
                    {
                        if (!string.IsNullOrEmpty(Image.ImgLocalPath))
                        {
                            var oldFileDirectory = Path.Combine(Directory.GetCurrentDirectory(), Image.ImgLocalPath);
                            FileInfo file = new FileInfo(oldFileDirectory);
                            if (file.Exists)
                            {
                                file.Delete();
                            }
                        }
                        string extension = Path.GetExtension(images.FileName);
                        string fileName = update.AbPhId + extension;
                        string filePath = @"wwwroot/About/" + fileName;
                        var filePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                        using (var fileStream = new FileStream(filePathDirectory, FileMode.Create))
                        {
                            images.CopyTo(fileStream);
                        }
                        var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";

                        Image.ImgLocalPath = filePath;
                        Image.ImgUrl = baseUrl + "/About/" + fileName;
                    }
                    Image.AboutId = 1;
                    _haganContext.AboutPhoto.Update(Image);
                    await _haganContext.SaveChangesAsync();
                    _responseDTO.Result = "Updated";
                }
                else
                {
                    _responseDTO.Message = "Image isn't exists";
                    _responseDTO.IsSuccess = false;
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
    }
}