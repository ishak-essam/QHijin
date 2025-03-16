using AutoMapper;
using BackEndHagan.Entities;
using BackEndHagan.Models.Dto;
using Microsoft.EntityFrameworkCore;
using QHijin.Entities;
using QHijin.Models.Dto;
using QHijin.Services.IService;

namespace QHijin.Services.Service
{
    public class Delivery_PeriodService: IDelivery_PeriodService
    {
        private HaganContext _haganContext;
        private ResponseDTO _responseDTO;
        private IMapper _mapper;

        public Delivery_PeriodService(HaganContext haganContext,
                IMapper mapper
            )
        {
            _haganContext = haganContext;
            _mapper = mapper;
            _responseDTO = new ResponseDTO();
        }

        public async Task<ResponseDTO> AddDelivery_Period(Delivery_PeriodDto Add)
        {
            try
            {
                var D_P =await _haganContext.Delivery_Period.FirstOrDefaultAsync(ele => ele.d_pN == 1);
                if (D_P != null)
                    Add.d_pN = 1;
                _haganContext.Delivery_Period.Update(_mapper.Map<Delivery_Period>(Add));
              await  _haganContext.SaveChangesAsync();
                _responseDTO.Result = "Created";
            }
            catch (Exception ex)
            {
                _responseDTO.Message = ex.Message.ToString();
                _responseDTO.IsSuccess = false;
            }
            return _responseDTO;
        }

        public async Task<ResponseDTO> GetDelivery_Periods()
        {
            try
            {
                var P_Ps =await _haganContext.Delivery_Period.FirstOrDefaultAsync(e => e.d_pN == 1);
                if (P_Ps != null)
                    _responseDTO.Result = P_Ps;
                else
                {
                    _responseDTO.Result = new Delivery_Period()
                    {
                        textAr="",
                        textEn="",
                        titleAr="",
                        titleEn="",
                        d_pN=0,
                        EmpId=0
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

        public async Task<ResponseDTO> RemoveDelivery_Period(int id)
        {
            try
            {
                var P_P =await _haganContext.Delivery_Period.FirstOrDefaultAsync(ele => ele.d_pN == id);
                if (P_P != null)
                {
                    _haganContext.Delivery_Period.Remove(P_P);
                  await  _haganContext.SaveChangesAsync();
                    _responseDTO.Result = "Deleted";
                }
                else
                {
                    _responseDTO.Message = "Delivery and  Period isn't exists";
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


        public async Task<ResponseDTO> UpdateDelivery_Period(Delivery_PeriodDto Delivery_PeriodDto)
        {
            try
            {
                var item = _mapper.Map<Delivery_Period>(Delivery_PeriodDto);
                var P_P =await _haganContext.Delivery_Period.FirstOrDefaultAsync(ele => ele.d_pN == 1);
                if (P_P != null)
                {
                    P_P.textEn = item.textEn;
                    P_P.textAr = item.textAr;
                    P_P.titleEn = item.titleEn; ;
                    P_P.titleAr = item.titleAr;
                    P_P.EmpId = item.EmpId;
                    _haganContext.Delivery_Period.Update(P_P);
                    _haganContext.SaveChanges();
                    _responseDTO.Result = "Updated";
                }
                else if (P_P == null)
                {
                    _haganContext.Database.ExecuteSqlRaw("DELETE FROM Delivery_Period");
                    _haganContext.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Delivery_Period', RESEED, 0)");
                    item.d_pN = 0;
                    await _haganContext.Delivery_Period.AddAsync(item);
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
