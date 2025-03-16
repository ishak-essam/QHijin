using AutoMapper;
using BackEndHagan.Entities;
using BackEndHagan.Models.Dto;
using BackEndHagan.Services.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndHagan.Services.Service
{
    public class TermsAndConditionService : ITermsAndConditionService
    {
        private  HaganContext _haganContext;
        private  ResponseDTO _responseDTO;
        private  IMapper _mapper;

        public TermsAndConditionService ( HaganContext haganContext
        ,  IMapper mapper

            )
        {
            _haganContext = haganContext;
            _mapper = mapper;
            _responseDTO = new ResponseDTO ();
        }

        public async Task<ResponseDTO> AddTermsAndCondition (  TermsAndConditionDto Add )
        {
            try
            {
                await  _haganContext.TermsAndConditions.AddAsync(_mapper.Map<TermsAndCondition> (Add));
                await _haganContext.SaveChangesAsync();
                _responseDTO.Result = "Created";
            }
            catch ( Exception ex )
            {
                _responseDTO.Message = ex.Message.ToString ();
                _responseDTO.IsSuccess = false;
            }
            return _responseDTO;
        }


        public async Task<ResponseDTO> GetTermsAndConditions ( )
        {
            try
            {
                var T_Cs = await _haganContext.TermsAndConditions.FirstOrDefaultAsync(ele=>ele.TcNo==1);
                if ( T_Cs != null )
                    _responseDTO.Result = T_Cs;
                else { 
                _responseDTO.Result = new TermsAndCondition()
                {
                    TcNo = 0,
                    EmpId = 0,
                    TextAr="",
                    TextEn="",
                };
                }
            }
            catch ( Exception ex )
            {
                _responseDTO.Message = ex.Message.ToString ();
                _responseDTO.IsSuccess = false;
            }
            return _responseDTO;
        }

        public async Task<ResponseDTO> RemoveTermsAndCondition ( int id )
        {
            try
            {
                var T_C = await  _haganContext.TermsAndConditions.FirstOrDefaultAsync(ele=>ele.TcNo== id);
                if ( T_C != null )
                {
                    _haganContext.TermsAndConditions.Remove (T_C);
                    _haganContext.SaveChanges ();
                    _responseDTO.Result ="Deleted";
                }
                else
                {
                    _responseDTO.Message = "Privacy And Policy isn't exists";
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

  
        public async Task<ResponseDTO> UpdateTermsAndCondition (  TermsAndCondition TermsAndConditionDto )
        {
            try
            {
                var T_C = await _haganContext.TermsAndConditions.FirstOrDefaultAsync(ele=>ele.TcNo== 1);
                if ( T_C != null )
                {
                    T_C.EmpId = TermsAndConditionDto.EmpId;
                    T_C.TextAr = TermsAndConditionDto.TextAr;
                    T_C.TextAr = TermsAndConditionDto.TextAr;
                    _haganContext.TermsAndConditions.Update (T_C);
                    await _haganContext.SaveChangesAsync ();
                    _responseDTO.Result = "Updated";
                }
                else if ( T_C == null ) {
                    _haganContext.Database.ExecuteSqlRaw("DELETE FROM TermsAndConditions");
                    _haganContext.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('TermsAndConditions', RESEED, 0)");
                    TermsAndConditionDto.TcNo = 0;
                    await _haganContext.TermsAndConditions.AddAsync (TermsAndConditionDto);
                    await _haganContext.SaveChangesAsync ();
                        _responseDTO.Result = "Add";
                }
            }
            catch ( Exception ex )
            {
                _responseDTO.Message = ex.Message.ToString ();
                _responseDTO.IsSuccess = false;
            }
            return _responseDTO;
        }
    }
}
