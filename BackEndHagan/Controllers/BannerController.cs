using AutoMapper;
using BackEndHagan.Entities;
using BackEndHagan.Extensions;
using BackEndHagan.Models.Dto;
using BackEndHagan.Services.IService;
using BackEndHagan.Services.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEndHagan.Controllers
{
    [ApiController]
    [Route ("api/banner")]
    public class BannerController : ControllerBase
    {

        private  HaganContext _haganContext;
        private  ResponseDTO _responseDTO;
        private  IBannerService _BannerService;
        private  IMapper _mapper;
        public BannerController ( HaganContext haganContext, IMapper mapper, IBannerService BannerService )
        {
            _BannerService = BannerService;
            _haganContext = haganContext;
            _mapper = mapper;
            _responseDTO = new ResponseDTO ();
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> GetUsers ( )
        {
            if ( User.GetRoles ().Contains (0) ) return Unauthorized ();
            ResponseDTO  emp=   await _BannerService.GetBanners ();
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }

        [HttpGet ("{id}")]
        public async Task<ActionResult<ResponseDTO>> GetBannerById ( int id )
        {
            if ( User.GetRoles ().Contains (0) ) return Unauthorized ();
            ResponseDTO  emp=   await  _BannerService.GetBannerById (id);
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }
        [HttpPost]
        public async Task<ActionResult<ResponseDTO>> AddBanner(BannerDto Add)
        {
            if (User.GetRole()==0) return Unauthorized();
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
            Add.ImgUrl = baseUrl;
            try
            {
                if (Add.Image != null)
                {
                    ResponseDTO emp = await _BannerService.AddBanner(Add);
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

        /*
           public async Task<ActionResult<ResponseDTO>> AddBanner ( BannerDto Add )
           {
               if ( User.GetRoles ().Contains (0) ) return Unauthorized ();
               try
               {
                   Banner product=_mapper.Map<Banner>(Add);
                   if ( Add.Image != null )
                   {
                       var images=Add.Image;
                       BannerDto photo = new BannerDto ();
                       var ids = _haganContext.Banners.ToList();
                       var id=1;
                       if ( ids.Count == 0 ) { 
                           id = 1;
                       }
                       else
                           id = ids.Last ().BanarNo + 1;

                       if ( images != null )
                       {
                           string extension = Path.GetExtension (images.FileName) ;
                           string fileName=  id + extension;
                           string filePath=@"wwwroot/BannerImages/"+fileName;
                           var filePathDirectory=Path.Combine(Directory.GetCurrentDirectory(),filePath);
                           using ( var fileStream = new FileStream (filePathDirectory, FileMode.Create) )
                           {
                               images.CopyTo (fileStream);
                           }
                           var baseUrl=$"{HttpContext.Request.Scheme }://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                           product.ImgUrl = baseUrl + "/BannerImages/" + fileName;
                           product.ImgLocalPath = filePath;
                           product.SubTitleAr = Add.SubTitleAr;
                           product.SubTitleEn = Add.SubTitleEn;
                           _haganContext.Banners.Add (product);
                           _haganContext.SaveChanges ();
                       }
                       else
                       {
                           return BadRequest ();
                       }
                   }
                   _responseDTO.Result = "Banner Created";
               }
               catch ( Exception ex )
               {
                   _responseDTO.Message = ex.Message.ToString ();
                   _responseDTO.IsSuccess = false;
               }
               if ( _responseDTO.IsSuccess == false )
                   return BadRequest (_responseDTO);
               return Ok ( _responseDTO);
           }
         */

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<ResponseDTO>> UpdateBannerImageById(BannerDto update)
        {
            if (User.GetRole()==0) return Unauthorized();
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
            update.ImgUrl=baseUrl;
           ResponseDTO responseDTO = await _BannerService.UpdateBanner(update);
            if (responseDTO.IsSuccess == false)
                return BadRequest(responseDTO);
            return Ok(responseDTO);
        }

        /*
          public async Task<ActionResult<ResponseDTO>> UpdateBannerImageById ( BannerDto update )
          {
              if ( User.GetRoles ().Contains (0) ) return Unauthorized ();
                          var baseUrl=$"{HttpContext.Request.Scheme }://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";

              try
              {
                  Banner product=_mapper.Map<Banner>(update);
                  var Banner=_haganContext.Banners.FirstOrDefault(ele=>ele.BanarNo==update.BanarNo);
                  if ( Banner != null )
                  {

                      var images= update.Image;
                      if ( images != null )
                      {
                          if ( !string.IsNullOrEmpty (Banner.ImgLocalPath) )
                      {
                          var oldFileDirectory=Path.Combine(Directory.GetCurrentDirectory(),Banner.ImgLocalPath);
                          FileInfo  file=new FileInfo(oldFileDirectory);
                          if ( file.Exists )
                          {
                              file.Delete ();
                          }
                      }
                          string extension = Path.GetExtension (images.FileName) ;
                          string fileName=  update.BanarNo + extension;
                          string filePath=@"wwwroot/BannerImages/"+fileName;
                          var filePathDirectory=Path.Combine(Directory.GetCurrentDirectory(),filePath);
                          using ( var fileStream = new FileStream (filePathDirectory, FileMode.Create) )
                          {
                              images.CopyTo (fileStream);
                          }

                          Banner.ImgLocalPath = filePath;
                          Banner.ImgUrl = baseUrl + "/BannerImages/" + fileName;

                      }
                      Banner.EmpId = product.EmpId;
                      Banner.SubTitleAr = product.SubTitleAr;
                      Banner.SubTitleEn = product.SubTitleEn;
                      Banner.ParphAr = product.ParphAr;
                      Banner.ParphEn = product.ParphEn;
                      _haganContext.Banners.Update (Banner);
                      _haganContext.SaveChanges ();

                      _responseDTO.Result = "Updated";
                  }
                  else
                  {
                      _responseDTO.Message = "Banner isn't exists";
                      _responseDTO.IsSuccess = false;
                  }
              }
              catch ( Exception ex )
              {
                  _responseDTO.Message = ex.Message.ToString ();
                  _responseDTO.IsSuccess = false;
              }
              if ( _responseDTO.IsSuccess == false )
                  return BadRequest (_responseDTO);
              return Ok (_responseDTO);
          }
         */


        [HttpGet ("filterByEmpId")]
        [Authorize]

        public async Task<ActionResult<ResponseDTO>> GetBannerByEmpId ( int id )
        {
            if ( User.GetRoles ().Contains (0) ) return Unauthorized ();
            ResponseDTO  emp=   await  _BannerService.GetBannerByEmpId (id);
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }

        [HttpDelete ("{id}")]
        [Authorize]

        public async Task<ActionResult<ResponseDTO>> RemoveBanner ( int id )
        {
            if ( User.GetRoles ().Contains (0) ) return Unauthorized ();
            ResponseDTO  banner=   await  _BannerService.RemoveBanner (id);
            
            if ( banner.IsSuccess == false )
                return BadRequest (banner);
            return Ok (banner);
        }

    }
}