using AutoMapper;
using BackEndHagan.Entities;
using BackEndHagan.Models.Dto;
using Microsoft.EntityFrameworkCore;
using QHijin.Entities;
using QHijin.Models.Dto;
using QHijin.Services.IService;

namespace QHijin.Services.Service
{
    public class Contracting_PolicyService: IContracting_PolicyService
    {
        private HaganContext _haganContext;
    private ResponseDTO _responseDTO;
    private IMapper _mapper;

    public Contracting_PolicyService(HaganContext haganContext,
            IMapper mapper
        )
    {
        _haganContext = haganContext;
        _mapper = mapper;
        _responseDTO = new ResponseDTO();
    }

    public async Task<ResponseDTO> AddContracting_Policy(Contracting_PolicyDto Add)
    {
        try
        {
            var D_P =await _haganContext.Contracting_Policy.FirstOrDefaultAsync(ele => ele.con_pNo == 1);
            if (D_P != null)
                Add.con_pNo = 1;
            _haganContext.Contracting_Policy.Update(_mapper.Map<Contracting_Policy>(Add));
            _haganContext.SaveChanges();
            _responseDTO.Result = "Created";
        }
        catch (Exception ex)
        {
            _responseDTO.Message = ex.Message.ToString();
            _responseDTO.IsSuccess = false;
        }
        return _responseDTO;
    }

    public async Task<ResponseDTO> GetContracting_Policys()
    {
        try
        {
            var P_Ps =await _haganContext.Contracting_Policy.FirstOrDefaultAsync(e => e.con_pNo == 1);
            if (P_Ps != null)
                _responseDTO.Result = P_Ps;
            else
            {
                _responseDTO.Result = new Contracting_Policy()
                {
                    textAr = "",
                    textEn = "",
                    titleAr = "",
                    titleEn = "",
                    con_pNo = 0,
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

    public async Task<ResponseDTO> RemoveContracting_Policy(int id)
    {
        try
        {
            var P_P =await _haganContext.Contracting_Policy.FirstOrDefaultAsync(ele => ele.con_pNo == id);
            if (P_P != null)
            {
                _haganContext.Contracting_Policy.Remove(P_P);
              await  _haganContext.SaveChangesAsync();
                _responseDTO.Result = "Deleted";
            }
            else
            {
                _responseDTO.Message = "Contracting and Policy isn't exists";
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


    public async Task<ResponseDTO> UpdateContracting_Policy(Contracting_PolicyDto Contracting_PolicyDto)
    {
        try
        {
            var item = _mapper.Map<Contracting_Policy>(Contracting_PolicyDto);
            var P_P = await _haganContext.Contracting_Policy.FirstOrDefaultAsync(ele => ele.con_pNo == 1);
                if (P_P != null)
                {
                    P_P.textEn = item.textEn;
                    P_P.textAr = item.textAr;
                    P_P.titleEn = item.titleEn;
                    P_P.titleAr = item.titleAr;
                    P_P.EmpId = item.EmpId;
                    _haganContext.Contracting_Policy.Update(P_P);
                    _haganContext.SaveChanges();
                    _responseDTO.Result = "Updated";
                }
                else if (P_P == null)
                {
                    _haganContext.Database.ExecuteSqlRaw("DELETE FROM Contracting_Policy");
                    _haganContext.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Contracting_Policy', RESEED, 0)");
                    item.con_pNo = 0;
                    await _haganContext.Contracting_Policy.AddAsync(item);
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
