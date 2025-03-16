using AutoMapper;
using BackEndHagan.Entities;
using BackEndHagan.Models.Dto;
using Microsoft.EntityFrameworkCore;
using QHijin.Entities;
using QHijin.Models.Dto;
using QHijin.Services.IService;

namespace QHijin.Services.Service
{
    public class HowTobuyService : IHowTobuyService
    {
        private HaganContext _haganContext;
        private ResponseDTO _responseDTO;
        private IMapper _mapper;

        public HowTobuyService(HaganContext haganContext,
                IMapper mapper
            )
        {
            _haganContext = haganContext;
            _mapper = mapper;
            _responseDTO = new ResponseDTO();
        }

        public async Task<ResponseDTO> AddHowTobuy(HowTobuyDto Add)
        {
            try
            {
                var D_P = await _haganContext.HowTobuy.FirstOrDefaultAsync(ele => ele.HowBuyN == 1);
                if (D_P != null)
                    Add.HowBuyN = 1;
                _haganContext.HowTobuy.Update(_mapper.Map<HowTobuy>(Add));
                await _haganContext.SaveChangesAsync();
                _responseDTO.Result = "Created";
            }
            catch (Exception ex)
            {
                _responseDTO.Message = ex.Message.ToString();
                _responseDTO.IsSuccess = false;
            }
            return _responseDTO;
        }

        public async Task<ResponseDTO> GetHowTobuys()
        {
            try
            {
                var P_Ps =await _haganContext.HowTobuy.FirstOrDefaultAsync(e => e.HowBuyN == 1);
                if (P_Ps != null)
                    _responseDTO.Result = P_Ps;
                else
                {
                    _responseDTO.Result = new HowTobuy()
                    {
                        titleEn="",
                        textAr = "",
                        HowBuyN = 0,
                        EmpId = 0
                    };
                }
            }
            catch (Exception ex)
            {
                _responseDTO.Message = ex.Message.ToString();
                _responseDTO.IsSuccess = false;
            }
            return _responseDTO;
        }

        public async Task<ResponseDTO> RemoveHowTobuy(int id)
        {
            try
            {
                var P_P = await _haganContext.HowTobuy.FirstOrDefaultAsync(ele => ele.HowBuyN == id);
                if (P_P != null)
                {
                    _haganContext.HowTobuy.Remove(P_P);
                    await _haganContext.SaveChangesAsync();
                    _responseDTO.Result = "Deleted";
                }
                else
                {
                    _responseDTO.Message = "Privacy And Policy isn't exists";
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


        public async Task<ResponseDTO> UpdateHowTobuy(HowTobuyDto HowTobuyDto)
        {
            try
            {
                var item = _mapper.Map<HowTobuy>(HowTobuyDto);
                var P_P = await _haganContext.HowTobuy.FirstOrDefaultAsync(ele => ele.HowBuyN == 1);
                if (P_P != null)
                {
                    P_P.textEn = item.textEn;
                    P_P.textAr = item.textAr;
                    P_P.titleEn = item.titleEn;
                    P_P.titleAr = item.titleAr;
                    P_P.EmpId = item.EmpId;
                    _haganContext.HowTobuy.Update(P_P);
                    await _haganContext.SaveChangesAsync();
                    _responseDTO.Result = "Updated";
                }
                else if (P_P == null)
                {
                    _haganContext.Database.ExecuteSqlRaw("DELETE FROM HowTobuy");
                    _haganContext.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('HowTobuy', RESEED, 0)");
                    item.HowBuyN = 0;
                    await _haganContext.HowTobuy.AddAsync(item);
                    await _haganContext.SaveChangesAsync();
                    _responseDTO.Result = "Added";
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
