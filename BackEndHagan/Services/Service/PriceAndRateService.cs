using AutoMapper;
using BackEndHagan.Entities;
using BackEndHagan.Models.Dto;
using BackEndHagan.Services.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndHagan.Services.Service
{
    public class PriceAndRateService : IPriceAndRateService
    {
        private  HaganContext _haganContext;
        private  IMapper _mapper;

        private  ResponseDTO _responseDTO;
        public PriceAndRateService ( HaganContext haganContext,
          IMapper mapper)
        {
            _haganContext = haganContext;
            _mapper = mapper;
            _responseDTO = new ResponseDTO ();
        }

        public async Task<ResponseDTO> AddPriceAndRate ( [FromBody] PriceAndRateDto Add )
        {
            try
            {
                await _haganContext.PriceAndRates.AddAsync (_mapper.Map<PriceAndRate> (Add));
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

        public async Task<ResponseDTO> GetPriceAndRateById ( int PriceAndRateid )
        {
            try
            {
                var P_P = await _haganContext.PriceAndRates.FirstOrDefaultAsync(ele=>ele.PrNo== PriceAndRateid);
                if ( P_P != null ) {
                    _responseDTO.Result = P_P;
                }
                else
                {
                    _responseDTO.Message = "Privacy And Policy isn't exists";
                    _responseDTO.IsSuccess = false;
                }
            }
            catch ( Exception ex ) {
                _responseDTO.Message = ex.Message.ToString ();
                _responseDTO.IsSuccess = false;
            }
          return  _responseDTO;
        }

   

        public async Task<ResponseDTO> GetPriceAndRate ( )
        {
            try
            {
                var P_Ps = await _haganContext.PriceAndRates.FirstOrDefaultAsync(ele=>ele.PrNo==1);
                if ( P_Ps != null )
                {
                    _responseDTO.Result = P_Ps;
                }
                else
                {
                    _responseDTO.Result = new PriceAndRate()
                    {
                        EmpId = 0,
                        PrNo = 0,
                        TextAr = "",
                        TextEn = ""
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

        public async Task<ResponseDTO> RemovePriceAndRate ( int id )
        {
            try
            {
                var P_P = await _haganContext.PriceAndRates.FirstOrDefaultAsync(ele=>ele.PrNo== id);
                if ( P_P != null )
                {
                    _haganContext.PriceAndRates.Remove (P_P);
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

  
        public async Task<ResponseDTO> UpdatePriceAndRate ( [FromBody] PriceAndRateDto PriceAndRateDto )
        {
            try
            {
                var item=_mapper.Map<PriceAndRate>(PriceAndRateDto);
                var P_P = await _haganContext.PriceAndRates.FirstOrDefaultAsync(ele=>ele.PrNo== item.PrNo);
                if (P_P != null)
                {
                    P_P.TextEn = item.TextEn;
                    P_P.TextAr = item.TextAr;
                    P_P.EmpId = item.EmpId;
                    _haganContext.PriceAndRates.Update(P_P);
                    await _haganContext.SaveChangesAsync();
                    _responseDTO.Result = "Updated";
                }
                else if (P_P == null)
                {
                    _haganContext.Database.ExecuteSqlRaw("DELETE FROM PriceAndRate");
                    _haganContext.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('PriceAndRate', RESEED, 0)");
                    item.PrNo = 0;
                    await _haganContext.PriceAndRates.AddAsync(item);
                    await _haganContext.SaveChangesAsync();
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
