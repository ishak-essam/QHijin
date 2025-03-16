using AutoMapper;
using BackEndHagan.Entities;
using BackEndHagan.Models.Dto;
using BackEndHagan.Services.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndHagan.Services.Service
{
    public class TypeService : ITypeService
    {
        private  HaganContext _haganContext;
        private  ResponseDTO _responseDTO;
        private  IMapper _mapper;
        public TypeService ( HaganContext haganContext, IMapper mapper )
        {
            _haganContext = haganContext;
            _mapper = mapper;
            _responseDTO = new ResponseDTO ();
        }
        public async Task<ResponseDTO> GetTypeById ( int Typeid )
        {
            try
            {
                var Type=await _haganContext.Types.FirstOrDefaultAsync(x=>x.TypeId==Typeid);
                if ( Type != null )
                {
                    _responseDTO.Result = Type;
                }
                else
                {
                    _responseDTO.IsSuccess = false;
                    _responseDTO.Message = "It isn't exist";
                }
            }
            catch ( Exception ex )
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message.ToString ();
            }
            return _responseDTO;
        }

        public async Task<ResponseDTO> GetTypeByName ( string name = "" )
        {
            try
            {
                IQueryable<Entities.Type> query = _haganContext.Types;
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
        public async Task<ResponseDTO> GetTypes ( )
        {
            try
            {
                var items = await _haganContext.Types.ToListAsync();
                _responseDTO.Result = items;
            }
            catch ( Exception ex )
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message.ToString ();
            }
            return _responseDTO;
        }
        public async Task<ResponseDTO> RemoveType ( int TypeId )
        {
            try
            {
                var Type=await _haganContext.Types.FirstOrDefaultAsync(u=>u.TypeId==TypeId);
                if ( Type != null )
                {
                    Type.IsActive = false;
                    _haganContext.Types.Update (Type);
                    _haganContext.SaveChanges ();
                    _responseDTO.Result = "Deleted";
                }
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
        public async Task<ResponseDTO> ReNewType ( int TypeId )
        {
            try
            {
                var Type=await _haganContext.Types.FirstOrDefaultAsync(u=>u.TypeId==TypeId);

                if ( Type != null )
                {
                    Type.IsActive = true;
                    _haganContext.Types.Update (Type);
                    _haganContext.SaveChanges ();
                    _responseDTO.Result = "Renew";
                }
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
        public async Task<ResponseDTO> UpdateType (  TypeDto TypeDto )
        {
            try
            {
                var item=_mapper.Map<Entities.Type>(TypeDto);
                var Type = await _haganContext.Types.FirstOrDefaultAsync(u => u.TypeId == item.TypeId);
                if ( Type != null )
                {
                    Type.Name = item.Name;
                    Type.IsActive = item.IsActive;
                    _haganContext.Types.Update (Type);
                    await _haganContext.SaveChangesAsync ();
                    _responseDTO.Result = "Updated";
                }
                else
                {
                    _responseDTO.Message = "item isn't exists ";
                    _responseDTO.IsSuccess = false;
                }
            }
            catch ( Exception ex )
            {
                _responseDTO.Message = ex.Message;
                _responseDTO.IsSuccess = false;
            }
            return _responseDTO;
        }
        public async Task<ResponseDTO> AddType ( TypeDto TypeDto )
        {
            try
            {
                await _haganContext.Types.AddAsync(_mapper.Map<Entities.Type> (TypeDto));
                await _haganContext.SaveChangesAsync ();
                _responseDTO.Result = "Type Created";
            }
            catch ( Exception ex )
            {
                _responseDTO.Message = ex.Message;
                _responseDTO.IsSuccess = false;
            }
            return _responseDTO;
        }
    }
}