using AutoMapper;
using BackEndHagan.Entities;
using BackEndHagan.Models.Dto;
using BackEndHagan.Services.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndHagan.Services.Service
{
    public class SalaryService: ISalaryService
    {
        private  IMapper _mapper;

        private  HaganContext _haganContext;
        private  ResponseDTO _responseDTO;
        public SalaryService ( IMapper mapper, HaganContext haganContext
            )
        {
            _haganContext = haganContext;
            _mapper = mapper;
            _responseDTO = new ResponseDTO ();
        }
        public async Task<ResponseDTO> GetSalaryByEmpId ( int EmpId ) {
            try
            {
                var P_P = await _haganContext.Salaries.FirstOrDefaultAsync(ele=>ele.EmpId== EmpId);
                if ( P_P != null )
                {
                    _responseDTO.Result = P_P;
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
        public async Task<ResponseDTO> AddSalary ( SalaryDto Add )
        {
            try
            {
                await _haganContext.Salaries.AddAsync (_mapper.Map<Salary>(Add));
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
        public async Task<ResponseDTO> GetSalaryById ( int SalaryNo )
        {
            try
            {
                var P_P = await  _haganContext.Salaries.FirstOrDefaultAsync(ele=>ele.SalaryNo== SalaryNo);
                if ( P_P != null )
                {
                    _responseDTO.Result = P_P;
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



        public async Task<ResponseDTO> GetSalarys ( )
        {
            try
            {
                var P_Ps = await _haganContext.Salaries.ToListAsync();
                if ( P_Ps != null )
                {
                    _responseDTO.Result = P_Ps;
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

        public async Task<ResponseDTO> RemoveSalary ( int id )
        {
            try
            {
                var P_P = await _haganContext.Salaries.FirstOrDefaultAsync(ele=>ele.SalaryNo== id);
                if ( P_P != null )
                {
                    _haganContext.Salaries.Remove (P_P);
                    await _haganContext.SaveChangesAsync ();
                    _responseDTO.Result = "Deleted";
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


        public async Task<ResponseDTO> UpdateSalary (  SalaryDto SalaryDto )
        {
            try
            {
                var Salary = await _haganContext.Salaries.FirstOrDefaultAsync(ele=>ele.SalaryNo== SalaryDto.SalaryNo);
                if ( Salary != null )
                {
                    Salary.EmpId = SalaryDto.EmpId;
                    Salary.Money = SalaryDto.Money;
                    Salary.Date = SalaryDto.Date;
                    _haganContext.Salaries.Update (Salary);
                    await _haganContext.SaveChangesAsync ();
                    _responseDTO.Result = "Updated";
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
    }
}