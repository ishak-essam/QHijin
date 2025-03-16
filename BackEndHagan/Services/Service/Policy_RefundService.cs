using AutoMapper;
using BackEndHagan.Entities;
using BackEndHagan.Models.Dto;
using Microsoft.EntityFrameworkCore;
using QHijin.Entities;
using QHijin.Models.Dto;
using QHijin.Services.IService;

namespace QHijin.Services.Service
{
    public class Policy_RefundService : IPolicy_RefundService
    {
        private HaganContext _haganContext;
        private ResponseDTO _responseDTO;
        private IMapper _mapper;

        public Policy_RefundService(HaganContext haganContext,
                IMapper mapper
            )
        {
            _haganContext = haganContext;
            _mapper = mapper;
            _responseDTO = new ResponseDTO();
        }

        public async Task<ResponseDTO> AddPolicy_Refund(Policy_RefundDto Add)
        {
            try
            {
                var D_P = await _haganContext.Policy_Refund.FirstOrDefaultAsync(ele => ele.p_refN == 1);
                if (D_P != null)
                    Add.p_refN = 1;
                _haganContext.Policy_Refund.Update(_mapper.Map<Policy_Refund>(Add));
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

        public async Task<ResponseDTO> GetPolicy_Refunds()
        {
            try
            {
                var P_Ps = await _haganContext.Policy_Refund.FirstOrDefaultAsync(e => e.p_refN == 1);
                if (P_Ps != null)
                    _responseDTO.Result = P_Ps;
                else
                {
                    _responseDTO.Result = new Policy_Refund()
                    {
                        EmpId = 0,
                        p_refN = 0,
                        textEn="",
                        textAr="",
                        titleAr="",
                        titleEn = ""
                    
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

        public async Task<ResponseDTO> RemovePolicy_Refund(int id)
        {
            try
            {
                var P_P = await _haganContext.Policy_Refund.FirstOrDefaultAsync(ele => ele.p_refN == id);
                if (P_P != null)
                {
                    _haganContext.Policy_Refund.Remove(P_P);
                    await _haganContext.SaveChangesAsync();
                    _responseDTO.Result = "Deleted";
                }
                else
                {
                    _responseDTO.Message = "Policy and Refund isn't exists";
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


        public async Task<ResponseDTO> UpdatePolicy_Refund(Policy_RefundDto Policy_RefundDto)
        {
            try
            {
                var item = _mapper.Map<Policy_Refund>(Policy_RefundDto);
                var P_P = await _haganContext.Policy_Refund.FirstOrDefaultAsync(ele => ele.p_refN == 1);
                if (P_P != null)
                {
                    P_P.textEn = item.textEn;
                    P_P.textAr = item.textAr;
                    P_P.titleEn = item.titleEn;
                    P_P.titleAr = item.titleAr;
                    P_P.EmpId = item.EmpId;
                    _haganContext.Policy_Refund.Update(P_P);
                    _haganContext.SaveChanges();
                    _responseDTO.Result = "Updated";
                }
                else if (P_P == null)
                {
                        _haganContext.Database.ExecuteSqlRaw("DELETE FROM Policy_Refund");
                        _haganContext.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Policy_Refund', RESEED, 0)");
                        item.p_refN = 0;
                    await _haganContext.Policy_Refund.AddAsync(item);
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
