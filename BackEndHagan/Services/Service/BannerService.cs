using AutoMapper;
using BackEndHagan.Entities;
using BackEndHagan.Models.Dto;
using BackEndHagan.Services.IService;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BackEndHagan.Services.Service
{
    public class BannerService : IBannerService
    {
        private  HaganContext _haganContext;
        private  ResponseDTO _responseDTO;
        private  IMapper _mapper;
        public BannerService ( HaganContext haganContext, IMapper mapper )
        {
            _haganContext = haganContext;
            _mapper = mapper;
            _responseDTO = new ResponseDTO ();
        }
        public async Task<ResponseDTO> AddBanner ( BannerDto Add )
        {
            var images = Add.Image;
            Banner product = _mapper.Map<Banner>(Add);
            var ids =await _haganContext.Banners.ToListAsync();
            var id = 1;
            if (ids.Count == 0)
            {
                id = 1;
            }
            else
                id = ids.Last().BanarNo + 1;
            if (images != null)
            {
                string extension = Path.GetExtension(images.FileName);
                string fileName = id + extension;
                string filePath = @"wwwroot/BannerImages/" + fileName;
                var filePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                using (var fileStream = new FileStream(filePathDirectory, FileMode.Create))
                {
                    images.CopyTo(fileStream);
                }
                product.ImgUrl = Add.ImgUrl + "/BannerImages/" + fileName;
                product.ImgLocalPath = filePath;
                product.SubTitleAr = Add.SubTitleAr;
                product.SubTitleEn = Add.SubTitleEn;

                _haganContext.Banners.Add(product);
               await _haganContext.SaveChangesAsync();
                _responseDTO.Result = "Banner Created";
                
            }
            return _responseDTO;
        }

        public async Task<ResponseDTO> GetBannerById ( int Bannerid )
        {
            try
            {
                var item=await _haganContext.Banners.FirstOrDefaultAsync(u=>u.BanarNo==Bannerid);
                if ( item != null )
                    _responseDTO.Result = item;
                else
                {
                    _responseDTO.Message = "it isn't exist";
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

        public async Task<ResponseDTO> GetBannerByEmpId ( int  id  )
        {
            try
            {
                var item=await _haganContext.Banners.FirstOrDefaultAsync(u=>u.EmpId==id);
                if ( item != null )
                    _responseDTO.Result = item;
                else
                {
                    _responseDTO.Message = "it isn't exist";
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

        public async Task<ResponseDTO> GetBanners ( )
        {
            try
            {
               _responseDTO.Result =await _haganContext.Banners.ToListAsync ();
            }
            catch ( Exception ex )
            {
                _responseDTO.Message = ex.Message.ToString ();
                _responseDTO.IsSuccess = false;
            }
            return _responseDTO;
        }

        public async Task<ResponseDTO> RemoveBanner ( int id )
        {
            try {
                var item=await _haganContext.Banners.FirstOrDefaultAsync(u=>u.BanarNo==id);
                if ( item != null )
                {
                    var oldFileDirectory=Path.Combine(Directory.GetCurrentDirectory(),item.ImgLocalPath!);
                    FileInfo  file=new FileInfo(oldFileDirectory);
                    if ( file.Exists )
                    {
                        file.Delete ();
                    }
                    _haganContext.Banners.Remove (item);
                    _haganContext.SaveChanges ();
                    _responseDTO.Result = "Deleted";
                }
                else
                {
                    _responseDTO.Message = "it isn't exist";
                    _responseDTO.IsSuccess = false;
                }
            }
            catch ( Exception ex ) {
                _responseDTO.Message = ex.Message.ToString ();
                _responseDTO.IsSuccess = false;
            }
            return _responseDTO;
        }

        public async Task<ResponseDTO> UpdateBanner ( BannerDto update)
        {
            try
            {
                Banner product = _mapper.Map<Banner>(update);
                var Banner =await _haganContext.Banners.FirstOrDefaultAsync(ele => ele.BanarNo == update.BanarNo);
                if (Banner != null)
                {
                    var images = update.Image;
                    if (images != null)
                    {
                        if (!string.IsNullOrEmpty(Banner.ImgLocalPath))
                        {
                            var oldFileDirectory = Path.Combine(Directory.GetCurrentDirectory(), Banner.ImgLocalPath);
                            FileInfo file = new FileInfo(oldFileDirectory);
                            if (file.Exists)
                            {
                                file.Delete();
                            }
                        }
                        string extension = Path.GetExtension(images.FileName);
                        string fileName = update.BanarNo + extension;
                        string filePath = @"wwwroot/BannerImages/" + fileName;
                        var filePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                        using (var fileStream = new FileStream(filePathDirectory, FileMode.Create))
                        {
                            images.CopyTo(fileStream);
                        }
                        Banner.ImgLocalPath = filePath;
                        Banner.ImgUrl = product.ImgUrl + "/BannerImages/" + fileName;
                    }
                    Banner.EmpId = product.EmpId;
                    Banner.SubTitleAr = product.SubTitleAr;
                    Banner.SubTitleEn = product.SubTitleEn;
                    Banner.ParphAr = product.ParphAr;
                    Banner.ParphEn = product.ParphEn;
                    _haganContext.Banners.Update(Banner);
                    await _haganContext.SaveChangesAsync();

                    _responseDTO.Result = "Updated";
                }
                else
                {
                    _responseDTO.Message = "Banner isn't exists";
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
