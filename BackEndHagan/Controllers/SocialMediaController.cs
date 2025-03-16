using AutoMapper;
using BackEndHagan.Entities;
using BackEndHagan.Models.Dto;
using BackEndHagan.Services.IService;
using BackEndHagan.Services.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace BackEndHagan.Controllers
{
    [ApiController]
    [Route ("api/socialmedia")]
    public class SocialMediaController : ControllerBase
    {

        private  HaganContext _haganContext;
        private  ResponseDTO _responseDTO;
        private  ISocialMediaService _SocialMediaService;
        private  IMapper _mapper;
        public SocialMediaController ( HaganContext haganContext, IMapper mapper, ISocialMediaService SocialMediaService )
        {
            _SocialMediaService = SocialMediaService;
            _haganContext = haganContext;
            _mapper = mapper;
            _responseDTO = new ResponseDTO ();
        }


        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> GetSocialMedias ( )
        {
            ResponseDTO  emp= await _SocialMediaService.GetSocialMedias ();
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }

        [HttpGet ("{id}")]
        [Authorize]
        public async Task<ActionResult<ResponseDTO>> GetSocialMediaById ( int Userid )
        {
            ResponseDTO  emp= await _SocialMediaService.GetSocialMediaById (Userid);
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }


        [HttpPost]
        [Authorize]
       public async Task<ActionResult<ResponseDTO>> AddSocialMedia ( SocialMediaDto Add )
        {
            try
            {
                SocialMedia product= _mapper.Map<SocialMedia>(Add);
                if ( Add.Image != null )
                {
                    var images=Add.Image;
                    SocialMediaDto photo = new SocialMediaDto ();
                    var ids =await _haganContext.SocialMedia.CountAsync();
                    if ( ids < 1 )
                        ids = 1;
                    else
                        ids = ids + 1;
                    if ( images != null )
                    {
                        string extension = Path.GetExtension (images.FileName) ;
                        string fileName=  ids + extension;
                        string filePath=@"wwwroot/SocialMediaImages/"+fileName;
                        var filePathDirectory=Path.Combine(Directory.GetCurrentDirectory(),filePath);
                        using ( var fileStream = new FileStream (filePathDirectory, FileMode.Create) )
                        {
                            images.CopyTo (fileStream);
                        }
                        var baseUrl=$"{HttpContext.Request.Scheme }://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                        product.ImgUrl = baseUrl + "/SocialMediaImages/" + fileName;
                        product.ImgLocalPath = filePath;
                       await _haganContext.SocialMedia.AddAsync (product);
                       await _haganContext.SaveChangesAsync ();
                    }
                    else
                    {
                        product.ImgUrl = "https://placehold.co/600*400";
                    await    _haganContext.SocialMedia.AddAsync (product);
                        await _haganContext.SaveChangesAsync ();
                    }
                _responseDTO.Result = "Social Media Created";
                }
                else
                return BadRequest ( );
            }
            catch ( Exception ex )
            {
                _responseDTO.Message = ex.Message.ToString ();
                _responseDTO.IsSuccess = false;
            }
            if ( _responseDTO.IsSuccess == false )
                return BadRequest ( _responseDTO);
            return Ok ( _responseDTO); 
         
        }

        [HttpPut ]
        [Authorize]
        public async Task<ActionResult<ResponseDTO>> UpdateItemImageById ( SocialMediaDto update )
        {
            try
            {
                SocialMedia product=_mapper.Map<SocialMedia>(update);
                var item=await _haganContext.SocialMedia.FirstOrDefaultAsync(ele=>ele.SocialMedNo==update.SocialMedNo);
                if ( item != null )
                {
                   
                    var images= update.Image;
                    SocialMediaDto photo = new SocialMediaDto ();
                    if ( images != null )
                    { if ( !string.IsNullOrEmpty (item.ImgLocalPath) )
                    {
                        var oldFileDirectory=Path.Combine(Directory.GetCurrentDirectory(),item.ImgLocalPath);
                        FileInfo  file=new FileInfo(oldFileDirectory);
                        if ( file.Exists )
                        {
                            file.Delete ();
                        }
                    }
                        string extension = Path.GetExtension (images.FileName) ;
                        string fileName=  update.SocialMedNo + extension;
                        string filePath=@"wwwroot/SocialMediaImages/"+fileName;
                        var filePathDirectory=Path.Combine(Directory.GetCurrentDirectory(),filePath);
                        using ( var fileStream = new FileStream (filePathDirectory, FileMode.Create) )
                        {
                            images.CopyTo (fileStream);
                        }
                        var baseUrl=$"{HttpContext.Request.Scheme }://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                        item.ImgLocalPath = filePath;
                        item.ImgUrl = baseUrl + "/SocialMediaImages/" + fileName;
                    }
                    item.EmpId = update.EmpId;
                    item.Name = update.Name;
                    item.Url = update.Url;
                    _haganContext.SocialMedia.Update (item);
                    await _haganContext.SaveChangesAsync ();

                    _responseDTO.Result = "Updated";
                }
                else
                {
                    _responseDTO.Message = "item isn't exists";
                    _responseDTO.IsSuccess = false;
                }
            }
            catch ( Exception ex )
            {
                _responseDTO.Message = ex.Message.ToString ();
                _responseDTO.IsSuccess = false;
            }
            return _responseDTO;
        }


        [HttpGet ("filterByName")]
        [Authorize]
        public async Task<ActionResult<ResponseDTO>> GetUserByName ( string name = "" )
        {
            ResponseDTO  emp= await _SocialMediaService.GetSocialMediaByName (name);
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }

        [HttpDelete ("{id}")]
        [Authorize]
        public async Task<ActionResult<ResponseDTO>> RemoveSocialMedia ( int id )
        {
            ResponseDTO  emp= await _SocialMediaService.RemoveSocialMedia (id);
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }
       
    }
}
