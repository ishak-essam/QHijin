using AutoMapper;
using BackEndHagan.Entities;
using BackEndHagan.Models.Dto;
using BackEndHagan.Services.IService;
using Microsoft.EntityFrameworkCore;

namespace BackEndHagan.Services.Service
{
    public class SocialMediaService : ISocialMediaService
    {
        private  HaganContext _haganContext;
        private  ResponseDTO _responseDTO;
        private  IMapper _mapper;
        public SocialMediaService ( HaganContext haganContext, IMapper mapper )
        {
            _haganContext = haganContext;
            _mapper = mapper;
            _responseDTO = new ResponseDTO ();
        }


        public Task<ResponseDTO> AddSocialMedia ( SocialMediaDto Add )
        {
            throw new NotImplementedException ();
        }

        public async Task<ResponseDTO> GetSocialMediaById ( int SocialMediaid )
        {
            try
            {
                var item=await _haganContext.SocialMedia.FirstOrDefaultAsync(u=>u.SocialMedNo==SocialMediaid);
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

        public async Task<ResponseDTO> GetSocialMediaByName ( string name = "" )
        {
            try
            {
                IQueryable<SocialMedia> query =_haganContext.SocialMedia;
                if ( query != null )
                {
                    query = query.Where (x => x.Name.Contains (name));
                    _responseDTO.Result = await query.ToListAsync();
                }
                return _responseDTO;
            }
            catch ( Exception ex )
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message.ToString ();
            }
            return _responseDTO;
        }

        public async Task<ResponseDTO> GetSocialMedias ( )
        {
            try
            {
               _responseDTO.Result = await _haganContext.SocialMedia.ToListAsync ();
            }
            catch ( Exception ex )
            {
                _responseDTO.Message = ex.Message.ToString ();
                _responseDTO.IsSuccess = false;
            }
            return _responseDTO;
        }

        public async Task<ResponseDTO> RemoveSocialMedia ( int id )
        {
            try {
                var item = await _haganContext.SocialMedia.FirstOrDefaultAsync(u=>u.SocialMedNo==id);
                if ( item != null )
                {
                    var oldFileDirectory=Path.Combine(Directory.GetCurrentDirectory(),item.ImgLocalPath!);
                    FileInfo  file=new FileInfo(oldFileDirectory);
                    if ( file.Exists )
                    {
                        file.Delete ();
                    }
                    _haganContext.SocialMedia.Remove (item);
                    await _haganContext.SaveChangesAsync ();
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

        public Task<ResponseDTO> UpdateSocialMedia ( SocialMediaDto SocialMediaDto )
        {
            throw new NotImplementedException ();
        }
    }
}
