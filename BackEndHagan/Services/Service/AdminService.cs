using AutoMapper;
using BackEndHagan.Entities;
using BackEndHagan.Models.Dto;
using BackEndHagan.Services.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace BackEndHagan.Services.Service
{


    public class AdminService : IAdminService
    {
        private  HaganContext _haganContext;
        private  ResponseDTO _responseDTO;
        private  IMapper _mapper;
        private const string PasswordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$";
        public AdminService ( HaganContext haganContext, IMapper mapper )
        {
            _mapper = mapper;
            _haganContext = haganContext;
            _responseDTO = new ResponseDTO ();
        }
        public async Task<ResponseDTO> GetAdminById ( int Adminid )
        {
            try
            {
                var Admin = await _haganContext.Admins.Include(e=>e.Emp).Include(e=>e.Titles).FirstOrDefaultAsync(x=>x.EmpId==Adminid);
                if ( Admin != null )
                {

                    _responseDTO.Result = Admin;
                }
                else
                {
                    _responseDTO.IsSuccess = false;
                    _responseDTO.Message = "It isn't exist";
                }
            }
            catch ( Exception ex )
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message.ToString ();
            }
            return _responseDTO;
        }

        public async Task<ResponseDTO> GetAdmins ( )
        {
            try
            {
                var items = await _haganContext.Admins.Include(e=>e.Emp).Include(e=>e.Titles).ToListAsync();
                _responseDTO.Result = items;
            }
            catch ( Exception ex )
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message.ToString ();
            }
        
            return _responseDTO;
        }
        public async Task<ResponseDTO> RemoveAdmin ( int AdminNo )
        {
            try
            {
                var Admin = await _haganContext.Admins.FirstOrDefaultAsync(u=>u.Id
                ==AdminNo);
                if ( Admin != null )
                {
                    var work = await  _haganContext.Work.FirstOrDefaultAsync(e=>e.TitleId==Admin.TitleId&&Admin.EmpId==e.EmpId);
                    _haganContext.Admins.Remove (Admin);
                    _haganContext.Work.Remove (work!);
                    await _haganContext.SaveChangesAsync();
                    _responseDTO.Result = "Deleted";
                }
                else
                {
                    _responseDTO.Message = "it isn't exist";
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
        public async Task<ResponseDTO> UpdateAdmin (  AdminDto AdminDto )
        {
            using var hmac =new HMACSHA512 ( );

            try
            {
                var Admin = await _haganContext.Admins.FirstOrDefaultAsync(u => u.EmpId == AdminDto.EmpId);
                if ( ExistTitle (AdminDto) == false )
                {
                    _responseDTO.Message = "Title isn't exist";
                    _responseDTO.IsSuccess = false;
                    return _responseDTO;
                }
                if ( IsPasswordStrong (AdminDto.Password) == false )
                {
                    _responseDTO.Message = "Password must be between 8 to 15 characters long and contain at least one lowercase letter, one uppercase letter, one digit, and one special character.";
                    _responseDTO.IsSuccess = false;
                    return _responseDTO;

                }
                if ( Admin != null )
                {
                    Admin.PasswordHash = hmac.ComputeHash (Encoding.UTF8.GetBytes (AdminDto.Password));
                    Admin.PasswordSalt = hmac.Key;
                    var work =_haganContext.Work.FirstOrDefault(e=> (e.TitleId==Admin.TitleId && Admin.EmpId==e.EmpId));
                    work!.TitleId = AdminDto.TitleId;
                    Admin.TitleId = AdminDto.TitleId;
                    _haganContext.Admins.Update (Admin);
                    _haganContext.Work.Update (work);
                    // Save changes
                    await _haganContext.SaveChangesAsync ();
                    _responseDTO.Result = "Updated";
                }
                else
                {
                    _responseDTO.Message = "Admin not found";
                    _responseDTO.IsSuccess = false;
                }
            }
            catch ( Exception ex )
            {
                _responseDTO.Message = ex.Message;
                _responseDTO.IsSuccess = false;
            }
            return _responseDTO;
        }
        public async Task<ResponseDTO> AddAdmin (  AdminDto AdminDto )
        {
            using var hmac =new HMACSHA512 ( );
            try
            {
                var emp= await _haganContext.Employees.FirstOrDefaultAsync (e=>e.EmpId== AdminDto.EmpId);
                var Admin = await _haganContext.Admins.FirstOrDefaultAsync(x=>x.EmpId==AdminDto.EmpId);
                if ( Admin != null )
                {
                    _responseDTO.Message = "Admin Aleady exist";
                    _responseDTO.IsSuccess = false;
                    return _responseDTO;

                }
                if ( emp == null ) {
                    _responseDTO.Message = "employee isn't exist";
                    _responseDTO.IsSuccess = false;
                    return _responseDTO;
                }
                if ( IsPasswordStrong (AdminDto.Password) == false )
                {
                    _responseDTO.Message = "Password must be between 8 to 15 characters long and contain at least one lowercase letter, one uppercase letter, one digit, and one special character.";
                    _responseDTO.IsSuccess = false;
                    return _responseDTO;

                }

                if ( ExistTitle (AdminDto) ==false ) {
                    _responseDTO.Message = "Title isn't exist";
                    _responseDTO.IsSuccess = false;
                    return _responseDTO;
                }
                var item=_mapper.Map<Admin>(AdminDto);
                item.PasswordHash = hmac.ComputeHash (Encoding.UTF8.GetBytes (AdminDto.Password));
                item.PasswordSalt = hmac.Key;
                item.Email = emp.Email;
                item.PasswordSalt = hmac.Key;
                item.TitleId = AdminDto.TitleId;
                await _haganContext.Admins.AddAsync (item);
                await _haganContext.Work.AddAsync (new Work { 
                    EmpId= item.EmpId,
                    TitleId=item.TitleId,
                    Id=0
                });
                await _haganContext.SaveChangesAsync ();
                _responseDTO.Result = "Admin Created";
            }
            catch ( Exception ex )
            {
                _responseDTO.Message = ex.Message;
                _responseDTO.IsSuccess = false;
            }
            return _responseDTO;
        }
        public bool IsPasswordStrong ( string password )
        {
            // Check if the password matches the pattern
            if ( !Regex.IsMatch (password, PasswordPattern) )
            {
                return false;
            }
            return true;
        }
        public bool ExistTitle ( AdminDto emp )
        {
            var titles=_haganContext.Titles.ToList();
            int titleId = emp.TitleId;
            bool isInList = titles.Any(title => title.TitleId == titleId);
            return isInList;
        }

    }
}
