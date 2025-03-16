using AutoMapper;
using BackEndHagan.Entities;
using BackEndHagan.Models.Dto;
using Microsoft.EntityFrameworkCore;
using QHijin.Entities;
using QHijin.Models;
using QHijin.Services.IService;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace QHijin.Services.Service
{
    public class AdsService : IAdsService
    {
        private HaganContext _haganContext;
        private ResponseDTO _responseDTO;
        private IMapper _mapper;
        public AdsService(HaganContext haganContext, IMapper mapper)
        {
            _haganContext = haganContext;
            _mapper = mapper;
            _responseDTO = new ResponseDTO();
        }
        public async Task<ResponseDTO> AddAds(AdsDto Add)
        {
            var images = Add.Image;
            Ads product = _mapper.Map<Ads>(Add);
            var ids = await _haganContext.Ads.ToListAsync();
            var id = 1;
            if (ids.Count == 0)
            {
                id = 1;
            }
            else
                id = ids.Last().AdId + 1;
            if (images != null)
            {
                string extension = Path.GetExtension(images.FileName);
                string fileName = id + extension;
                string filePath = @"wwwroot/Ads/" + fileName;
                var filePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                using (var fileStream = new FileStream(filePathDirectory, FileMode.Create))
                {
                    images.CopyTo(fileStream);
                }
                product.AdsImgUrl = Add.AdsImgUrl + "/Ads/" + fileName;
                product.AdsImgLocalPath = filePath;
                product.AdsType = Add.AdsType;
                product.Link = Add.Link;
                product.Text = Add.Text;
                product.EmpId = Add.EmpId;
                _haganContext.Ads.Add(product);
                await _haganContext.SaveChangesAsync();
                _responseDTO.Result = "Ads Created";

            }
            return _responseDTO;
        }

        public async Task<ResponseDTO> GetAds()
        {
            try
            {
                _responseDTO.Result = await _haganContext.Ads.ToListAsync();
            }
            catch (Exception ex)
            {
                _responseDTO.Message = ex.Message.ToString();
                _responseDTO.IsSuccess = false;
            }
            return _responseDTO;
        }

        public async Task<ResponseDTO> GetAdsById(int Adsid)
        {
            try
            {
                var Type = await _haganContext.Ads.FirstOrDefaultAsync(x => x.AdId == Adsid);
                if (Type != null)
                {
                    _responseDTO.Result = Type;
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

        public async Task<ResponseDTO> GetAdsByType(string type = "")
        {
            try
            {
                IQueryable<Ads> query = _haganContext.Ads;
                if (query != null)
                {
                    query = query.Where(x => x.AdsType.Contains(type));
                    _responseDTO.Result = await query.ToListAsync();
                }
                return _responseDTO;
            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message.ToString();
            }
            return _responseDTO;
        }

        public async Task<ResponseDTO> RemoveAds(int id)
        {
            try
            {
                var item = await _haganContext.Ads.FirstOrDefaultAsync(u => u.AdId == id);
                if (item != null)
                {
                    var oldFileDirectory = Path.Combine(Directory.GetCurrentDirectory(), item!.AdsImgLocalPath!);
                    FileInfo file = new FileInfo(oldFileDirectory);
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                    _haganContext.Ads.Remove(item);
                    await _haganContext.SaveChangesAsync();
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

        public async Task<ResponseDTO> UpdateAds(AdsDto update)
        {
            try
            {
                var Ads =await _haganContext.Ads.FirstOrDefaultAsync(ele => ele.AdId == update.AdId);
                if (Ads != null)
                {
                    var images = update.Image;
                    if (images != null)
                    {
                        if (!string.IsNullOrEmpty(Ads.AdsImgLocalPath))
                        {
                            var oldFileDirectory = Path.Combine(Directory.GetCurrentDirectory(), Ads.AdsImgLocalPath);
                            FileInfo file = new FileInfo(oldFileDirectory);
                            if (file.Exists)
                            {
                                file.Delete();
                            }
                        }
                        string extension = Path.GetExtension(images.FileName);
                        string fileName = update.AdId + extension;
                        string filePath = @"wwwroot/Ads/" + fileName;
                        var filePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                        using (var fileStream = new FileStream(filePathDirectory, FileMode.Create))
                        {
                            images.CopyTo(fileStream);
                        }

                        Ads.AdsImgLocalPath = filePath;
                        Ads.AdsImgUrl = update.AdsImgUrl + "/Ads/" + fileName;

                    }
                    Ads.EmpId = update.EmpId;
                    Ads.Text = update.Text;
                    Ads.Link = update.Link;
                    Ads.AdsType = update.AdsType;
                    _haganContext.Ads.Update(Ads);
                    await  _haganContext.SaveChangesAsync();
                    _responseDTO.Result = "Updated";
                }
                else
                {
                    _responseDTO.Message = "Ads isn't exists";
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
