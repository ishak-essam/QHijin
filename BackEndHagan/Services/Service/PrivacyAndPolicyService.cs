using AutoMapper;
using BackEndHagan.Entities;
using BackEndHagan.Models.Dto;
using BackEndHagan.Services.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BackEndHagan.Services.Service
{
    public class PrivacyAndPolicyService : IPrivacyAndPolicyService
    {
        private  HaganContext _haganContext;
        private  ResponseDTO _responseDTO;
        private  IMapper _mapper;

        public PrivacyAndPolicyService ( HaganContext haganContext,
                IMapper mapper
            )
        {
            _haganContext = haganContext;
            _mapper = mapper;
            _responseDTO = new ResponseDTO ();
        }

        public async Task<ResponseDTO> AddPrivacyAndPolicy (  PrivacyAndPolicyDto Add )
        {
            try
            {
                var PrivacyAndPolicy = await _haganContext.PrivacyAndPolicies.FirstOrDefaultAsync(ele=>ele.PpNo==1);
                if ( PrivacyAndPolicy != null )
                    Add.PpNo = 1;
                _haganContext.PrivacyAndPolicies.Update (_mapper.Map<PrivacyAndPolicy>(Add));
                await _haganContext.SaveChangesAsync ();
                    _responseDTO.Result = "Created";
            }
            catch ( Exception ex )
            {
                _responseDTO.Message = ex.Message.ToString ();
                _responseDTO.IsSuccess = false;
            }
            return _responseDTO;
        }

        public async Task<ResponseDTO> GetPrivacyAndPolicys ( )
        {
            try
            {
                var P_Ps = await _haganContext.PrivacyAndPolicies.FirstOrDefaultAsync(e=>e.PpNo==1);
                if ( P_Ps != null )
                    _responseDTO.Result = P_Ps;
                else
                {
                _responseDTO.Result = new PrivacyAndPolicy (){
                    TextEn="",
                    TextAr = "",
                    PpNo = 0
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

        public async Task<ResponseDTO> RemovePrivacyAndPolicy ( int id )
        {
            try
            {
                var P_P = await _haganContext.PrivacyAndPolicies.FirstOrDefaultAsync(ele=>ele.PpNo== id);
                if ( P_P != null )
                {
                    _haganContext.PrivacyAndPolicies.Remove (P_P);
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

  
        public async Task<ResponseDTO> UpdatePrivacyAndPolicy (  PrivacyAndPolicyDto PrivacyAndPolicyDto )
        {
            try
            {
                var item=_mapper.Map<PrivacyAndPolicy>(PrivacyAndPolicyDto);
                var P_P = await _haganContext.PrivacyAndPolicies.FirstOrDefaultAsync(ele=>ele.PpNo== 1);
                if ( P_P != null )
                {
                    P_P.TextEn = item.TextEn;
                    P_P.TextAr = item.TextAr;
                    P_P.EmpId = item.EmpId;
                    _haganContext.PrivacyAndPolicies.Update (P_P);
                    await _haganContext.SaveChangesAsync ();
                    _responseDTO.Result = "Updated";
                }
                else if ( P_P == null )
                {
                        _haganContext.Database.ExecuteSqlRaw ("DELETE FROM PrivacyAndPolicy");
                        _haganContext.Database.ExecuteSqlRaw ("DBCC CHECKIDENT ('PrivacyAndPolicy', RESEED, 0)");
                        item.PpNo = 0;
                    await _haganContext.PrivacyAndPolicies.AddAsync (item);
                    await _haganContext.SaveChangesAsync ();
                        _responseDTO.Result = "Added";
                 
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
