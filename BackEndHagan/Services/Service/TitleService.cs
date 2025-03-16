using AutoMapper;
using BackEndHagan.Entities;
using BackEndHagan.Models.Dto;
using BackEndHagan.Services.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndHagan.Services.Service
{
    public class TitleService : ITitleService
    {
        private  HaganContext _haganContext;
        private  ResponseDTO _responseDTO;
        private  IMapper _mapper;

        public TitleService ( HaganContext haganContext, IMapper mapper )
        {
            _mapper = mapper;
            _haganContext = haganContext;
            _responseDTO = new ResponseDTO ();
        }
        public async Task<ResponseDTO> AddTitle ( [FromBody] Title Add )
        {
            try {
               await _haganContext.Titles.AddAsync (Add);
                await _haganContext.SaveChangesAsync();
                _responseDTO.Result = "Created";
            }
           catch(Exception ex)   {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message.ToString ();
            }
            return _responseDTO;
        }

        public async Task<ResponseDTO> GetTitleById ( int Titleid )
        {
            try
            {
                var item= await _haganContext.Titles.FirstOrDefaultAsync(ele=>ele.TitleId==Titleid);
                if ( item != null )
                {
                    _responseDTO.Result = item;
                }
                else {
                    _responseDTO.IsSuccess = false;
                    _responseDTO.Message = "It isn't exists";
                }
            }
            catch ( Exception ex )
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message.ToString ();
            }
            return _responseDTO;
        }

        public async Task<ResponseDTO> GetTitleByEmp (  )
        {
            try
            {
                IQueryable<Title> query =_haganContext.Titles;
                if ( query != null )
                {
                    query = query.Where (x => x.IsAdmin== false);
                    _responseDTO.Result = await query.ToListAsync();
                }
            }
            catch ( Exception ex )
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message.ToString ();
            }
            return _responseDTO;
        }
        public async Task<ResponseDTO> GetTitleByAdmin ( )
        {
            try
            {
                IQueryable<Title> query =_haganContext.Titles;
                if ( query != null )
                {
                    query = query.Where (x => x.IsAdmin == true);
                    _responseDTO.Result = await query.ToListAsync();
                }
            }
            catch ( Exception ex )
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message.ToString ();
            }
            return _responseDTO;
        }
        
        public async Task<ResponseDTO> GetTitles ( )
        {
            try
            {
                var items= await _haganContext.Titles.Include(e=>e!.Works!).ThenInclude(ele=>ele.Emp).ToListAsync();
                _responseDTO.Result = items;
            }
            catch ( Exception ex )
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message.ToString ();
            }
            return _responseDTO;
        }

        public async Task<ResponseDTO> RemoveTitle ( int id )
        {
            try
            {
                var item=  _haganContext.Work;
                var admin=  _haganContext.Admins.Include(e=>e.Titles);
                bool isTitleIdUsed = await item.AnyAsync( ele => ele.TitleId == id);
                bool isTitleIdAdmin =await admin.AnyAsync(e=>e.TitleId==id);
                if ( item != null || admin !=null)
                {
                    if ( isTitleIdUsed == true || isTitleIdAdmin ==true) {
                        _responseDTO.IsSuccess = false;
                       if( isTitleIdUsed == true && isTitleIdAdmin == true)
                        _responseDTO.Message = "Can't delete title ,that's contains in admins and employee";
                        else if ( isTitleIdUsed == true  )
                            _responseDTO.Message = "Can't delete title ,that's contains  employee";
                       else
                            _responseDTO.Message = "Can't delete title ,that's contains in admins";
                     
                        
                        return _responseDTO;

                    }
                    var title =await _haganContext.Titles.FirstOrDefaultAsync(e=>e.TitleId==id);
                    if (title != null)
                    {
                        //_haganContext.Titles.Remove(title);
                        //_haganContext.SaveChangesAsync();
                        _haganContext.Database.ExecuteSqlRaw("DELETE FROM Titles WHERE TitleId = "+id);
                        _responseDTO.Result = "Deleted";
                    }
                    else
                    {
                        _responseDTO.IsSuccess = false;
                        _responseDTO.Message = "It isn't exists";

                    }
                }
                else
                {
                    _responseDTO.IsSuccess = false;
                    _responseDTO.Message = "It isn't exists";
                }
            }
            catch ( Exception ex )
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message.ToString ();
            }
            return _responseDTO;
        }

        public async Task<ResponseDTO> UpdateTitle ( [FromBody] Title TitleDto )
        {
            try
            {
                var item=await  _haganContext.Titles.FirstOrDefaultAsync(ele=>ele.TitleId==TitleDto.TitleId);
                if ( item != null )
                {
                    item.TitleName = TitleDto.TitleName;
                    item.IsAdmin = TitleDto.IsAdmin;
                    _haganContext.Titles.Update (item);
                    _haganContext.SaveChanges ();
                    _responseDTO.Result = "Updated";
                }
                else
                {
                    _responseDTO.IsSuccess = false;
                    _responseDTO.Message = "It isn't exists";
                }
            }
            catch ( Exception ex )
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message.ToString ();
            }
            return _responseDTO;
        }
    }
}
