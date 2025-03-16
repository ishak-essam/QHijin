using BackEndHagan.Entities;
using BackEndHagan.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BackEndHagan.Services.IService
{
    public interface ISalaryService
    {
        Task<ResponseDTO> GetSalarys ( );
        Task<ResponseDTO> GetSalaryById ( int Salaryid ); 
        Task<ResponseDTO> GetSalaryByEmpId ( int Empid ); 
        Task<ResponseDTO> UpdateSalary (  SalaryDto SalaryDto );
        Task<ResponseDTO> AddSalary (  SalaryDto Add );
        Task<ResponseDTO> RemoveSalary ( int id );
    }
}
