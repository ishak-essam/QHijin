using BackEndHagan.Entities;
using BackEndHagan.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BackEndHagan.Services.IService
{
    public interface IEmployeeService 
    {
        Task<ResponseDTO> RegisterEmp(EmployeeDto employee);
        Task<ResponseDTO> GetEmps ( );
        Task<ResponseDTO> GetEmpById ( int Empid );
        Task<ResponseDTO> GetEmpByName ( string name = "" );
        Task<ResponseDTO> GetEmpByPhone ( string phone = "" );
        Task<ResponseDTO> UpdateEmp (  EmployeeDto EmpDto );
        Task<ResponseDTO> UpdateItem ( ItemDto2 itemDto );
        Task<ResponseDTO> RemoveEmp (int id ); 
        Task<ResponseDTO> RenewEmp (int id ); 
    }
}
