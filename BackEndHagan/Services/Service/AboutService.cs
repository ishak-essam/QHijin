using AutoMapper;
using BackEndHagan.Entities;
using BackEndHagan.Models.Dto;
using BackEndHagan.Services.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndHagan.Services.Service
{
    public class AboutService : IAboutService
    {
        private  HaganContext _haganContext;
        private  IMapper _mapper;
        private  ResponseDTO _responseDTO;
        public AboutService ( HaganContext haganContext, IMapper mapper)
        {
            _haganContext = haganContext;
            _mapper = mapper;
            _responseDTO = new ResponseDTO ();
        }
        public async Task<ResponseDTO> AddAbout ( [FromBody] AboutDto Add )
        {
            try {
                await _haganContext.Abouts.AddAsync ( _mapper.Map<About>(Add) );
                await _haganContext.SaveChangesAsync ();
                _responseDTO.Result = "created";
            }
            catch( Exception ex )
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message.ToString ();
            }
            return _responseDTO;
        }
        public async Task<ResponseDTO> GetAboutById ( int Aboutid )
        {
            var item = await _haganContext.Abouts.FirstOrDefaultAsync(ele=>ele.AboutNo==Aboutid);
            try {
                if ( item != null )
                    _responseDTO.Result = item;
                else
                {
                    _responseDTO.IsSuccess = false;
                    _responseDTO.Message = "isn't exist";
                }
            }
            catch ( Exception ex )
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message.ToString ();
            }
            return _responseDTO;
        }


        public async Task<ResponseDTO> GetAbouts ( )
        {
            try
            {
                var about= await _haganContext.Abouts.Include(e=>e.AboutPhoto).FirstOrDefaultAsync(e=>e.AboutNo==1);
                  if(about!=null)
                _responseDTO.Result =about ;
                else
                {
                    _responseDTO.Result = new About()
                    {TextEn="",
                    TextAr="",
                    EmpId=0,
                    AboutNo=0
                    };
                }
            }
            catch ( Exception ex )
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message.ToString ();
            }
            return _responseDTO;
        }

        public async Task<ResponseDTO> RemoveAbout ( int id )
        {
            var item = await _haganContext.Abouts.FirstOrDefaultAsync(ele=>ele.AboutNo==id);
            try
            {
                if ( item != null )
                {
                    _haganContext.Database.ExecuteSqlRaw("DELETE FROM About");
                    _haganContext.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('About', RESEED, 0)");
                    _haganContext.Abouts.Remove (item);
                   await _haganContext.SaveChangesAsync();
                    _responseDTO.Result = "Delete";
                }
                else
                {
                    _responseDTO.IsSuccess = false;
                    _responseDTO.Message = "isn't exist";
                }
            }
            catch ( Exception ex )
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message.ToString ();
            }
            return _responseDTO;
        }

        public async Task<ResponseDTO> UpdateAbout ( [FromBody] AboutDto AboutDto )
        {
            var about=_mapper.Map<About>(AboutDto);
            var item = await  _haganContext.Abouts.FirstOrDefaultAsync(ele=>ele.AboutNo==1);
            try
            {
                if ( item != null )
                {
                    item.EmpId = AboutDto.EmpId;
                    item.TextEn = AboutDto.TextEn;
                    item.TextAr = AboutDto.TextAr;
                    item.Title = AboutDto.Title;
                    _haganContext.Abouts.Update (item);
                    await _haganContext.SaveChangesAsync ();
                    _responseDTO.Result = "Updated";
                }
                else if ( item == null )
                {

                    if ( _haganContext.Abouts.ToList ().Count == 0 )
                    {
                        about.AboutNo = 0;
                        _haganContext.Abouts.Add (about);
                        await _haganContext.SaveChangesAsync();
                        _responseDTO.Result = "created";
                    }
                    else if ( _haganContext.Abouts.ToList ().Count > 0 )
                    {
                        _haganContext.Database.ExecuteSqlRaw ("DELETE FROM About");
                        _haganContext.Database.ExecuteSqlRaw ("DBCC CHECKIDENT ('About', RESEED, 0)");

                        //  "DBCC CHECKIDENT ('YourEntities', RESEED, 0)"
                        about.AboutNo = 0;
                        await _haganContext.Abouts.AddAsync (about);
                        await _haganContext.SaveChangesAsync ();
                        _responseDTO.Result = "created";
                    }
                }
               
            }
            catch ( Exception ex )
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message.ToString ();
            }
            return _responseDTO;
        }
        public async Task<ResponseDTO> RemovePhotoAbout(int id)
        {
            try
            {
                var item = await _haganContext.AboutPhoto.FirstOrDefaultAsync(u => u.AbPhId == id);
                if (item != null)
                {
                    var oldFileDirectory = Path.Combine(Directory.GetCurrentDirectory(), item.ImgLocalPath!);
                    FileInfo file = new FileInfo(oldFileDirectory);
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                    _haganContext.AboutPhoto.Remove(item);
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
    }
}
