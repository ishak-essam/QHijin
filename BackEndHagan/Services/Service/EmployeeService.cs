using AutoMapper;
using BackEndHagan.Entities;
using BackEndHagan.Models.Dto;
using BackEndHagan.Services.IService;
using Microsoft.EntityFrameworkCore;
namespace BackEndHagan.Services.Service
{
    public class EmployeeService : IEmployeeService
    {
        private HaganContext _haganContext;
        private ResponseDTO _responseDTO;
        private IMapper _mapper;

        public EmployeeService(HaganContext haganContext, IMapper mapper
           )
        {
            _haganContext = haganContext;
            _mapper = mapper;
            _responseDTO = new ResponseDTO();
        }
        public async Task<ResponseDTO> GetEmpById(int Empid)
        {
            try
            {
                var user = await _haganContext.Employees.Include(e => e.Works).ThenInclude(e => e.Title).Include(e => e.PrivacyAndPolicies)
                    .Include(e => e.PrivacyAndPolicies).Include(e => e.PriceAndRates)
                    .Include(e => e.PrivacyAndPolicies).
                    FirstOrDefaultAsync(x => x.EmpId == Empid);
                if (user != null)
                {
                    var userEmp = new
                    {
                        empId = user.EmpId,
                        fullName = user.FullName,
                        phone = user.Phone,
                        email = user.Email,
                        abouts = user.Abouts,
                        banners = user.Banners,
                        employeeItems = user.EmployeeItems,
                        priceAndRates = user.PriceAndRates,
                        salaries = user.Salaries,
                        privacyAndPolicies = user.PrivacyAndPolicies,
                        socialMedia = user.SocialMedia,
                        Status = user.Status,
                        termsAndConditions = user.TermsAndConditions,
                        works = user.Works.Select(work => new
                        {
                            id = work.Id,
                            titleId = work.TitleId,
                            empId = work.EmpId,
                            title = new
                            {
                                work!.Title!.TitleId,
                                work.Title.TitleName,
                                work.Title.IsAdmin,
                                works = new List<object>()
                            }
                        })
                    };

                    _responseDTO.Result = userEmp;
                }
                else
                {
                    _responseDTO.IsSuccess = false;
                    _responseDTO.Message = "It isn't exist";
                }
            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message.ToString();
            }
            return _responseDTO;
        }
        public async Task<ResponseDTO> GetEmpByName(string name = "")
        {
            try
            {
                IQueryable<Employee> query = _haganContext.Employees;
                if (query != null)
                {
                    query = query.Where(x => x.FullName.Contains(name));
                    _responseDTO.Result = await query.ToListAsync();
                }
                return _responseDTO;
            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message.ToString();
            }
            return _responseDTO;
        }
        public async Task<ResponseDTO> GetEmpByPhone(string phone = "")
        {
            try
            {
                IQueryable<Employee> query = _haganContext.Employees;
                if (query != null)
                {
                    query = query.Where(x => x.Phone.Contains(phone));
                    _responseDTO.Result = await query.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message.ToString();
            }
            return _responseDTO;
        }
        public async Task<ResponseDTO> GetEmps()
        {
            try
            {
                var user = await _haganContext.Employees.Include(e => e.Works).ThenInclude(ele => ele.Title).ToListAsync();
                _responseDTO.Result = user.Select(user => new
                {
                    empId = user.EmpId,
                    fullName = user.FullName,
                    phone = user.Phone,
                    Status = user.Status,
                    email = user.Email,
                    abouts = user.Abouts,
                    banners = user.Banners,
                    employeeItems = user.EmployeeItems,
                    priceAndRates = user.PriceAndRates,
                    salaries = user.Salaries,
                    privacyAndPolicies = user.PrivacyAndPolicies,
                    socialMedia = user.SocialMedia,
                    termsAndConditions = user.TermsAndConditions,
                    works = user.Works.Select(work => new
                    {
                        id = work.Id,
                        titleId = work.TitleId,
                        empId = work.EmpId,
                        title = new
                        {
                            work!.Title!.TitleId,
                            work.Title.TitleName,
                            work.Title.IsAdmin,
                            works = new List<object>() // or you can omit this if you don't want to include works here
                        }, // Assuming you don't want to include employee details here
                    }).ToList()
                }).ToList();

            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message.ToString();
            }

            return _responseDTO;
        }

        public async Task<ResponseDTO> RemoveEmp(int Id)
        {
            try
            {
                var user = await _haganContext.Employees.FirstOrDefaultAsync(u => u.EmpId == Id);
                if (user != null)
                {
                    user.Status = false;
                    _haganContext.Employees.Update(user);
                    await _haganContext.SaveChangesAsync();
                    _responseDTO.Result = "Deleted";
                }
                else
                {
                    _responseDTO.Message = "it isn't exist";
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
        public async Task<ResponseDTO> RenewEmp(int Id)
        {
            try
            {
                var user = await _haganContext.Employees.FirstOrDefaultAsync(u => u.EmpId == Id);
                if (user != null)
                {
                    user.Status = true;
                    _haganContext.Employees.Update(user);
                    await _haganContext.SaveChangesAsync();
                    _responseDTO.Result = "Renew";
                }
                else
                {
                    _responseDTO.Message = "it isn't exist";
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
        public async Task<ResponseDTO> UpdateEmp(EmployeeDto EmpDto)
        {
            try
            {
                Employee emp = _mapper.Map<Employee>(EmpDto);
                var user = await _haganContext.Employees.Include(e => e.Works).FirstOrDefaultAsync(u => u.EmpId == emp.EmpId);
                if (ExistTitle(EmpDto) == false)
                {
                    _responseDTO.Message = "Title not found";
                    _responseDTO.IsSuccess = false;
                    return _responseDTO;
                }

                if (user == null)
                {
                    _responseDTO.Message = "Emp not found";
                    _responseDTO.IsSuccess = false;
                    return _responseDTO;
                }
                if (DoesNotExist(emp.FullName, emp.Email, user.EmpId) == false)
                {
                    _responseDTO.Message = "Employee is already exists";
                    _responseDTO.IsSuccess = false;
                    return _responseDTO;
                }
                user.FullName = EmpDto.FullName;
                user.Email = EmpDto.Email;
                user.Phone = EmpDto.Phone;
                _haganContext.Employees.Update(user);
                await _haganContext.SaveChangesAsync();

                List<Work> works = user.Works;
                var count = works.Count > EmpDto.TitleIds.Count ? works.Count : EmpDto.TitleIds.Count;
                var x = EmpDto.TitleIds.Count;
                var y = works.Count;
                for (var i = 0; i < count; i++)
                {

                    if ((y > i && x > i))
                    {
                        works[i].TitleId = EmpDto.TitleIds[i];
                        _haganContext.Work.Update(works[i]);
                    }
                    else if (x > i && y < x)
                    {
                        await _haganContext.Work.AddAsync(new Work
                        {
                            Id = 0,
                            EmpId = user.EmpId,
                            TitleId = EmpDto.TitleIds[i],
                        });
                    }
                    else if (y > i && y > x)
                    {
                        _haganContext.Work.Remove(works[i]);
                    }

                    await _haganContext.SaveChangesAsync();
                }
                _responseDTO.Result = "Updated";
            }
            catch (Exception ex)
            {
                _responseDTO.Message = ex.Message;
                _responseDTO.IsSuccess = false;
            }
            return _responseDTO;
        }

        public async Task<ResponseDTO> RegisterEmp(EmployeeDto employee)
        {
            try
            {
                Employee emp = _mapper.Map<Employee>(employee);
                if (ExistTitle(employee) == false)
                {
                    _responseDTO.Message = "Title isn't exists";
                    _responseDTO.IsSuccess = false;
                    return _responseDTO;
                }
                if (DoesNotExist(emp.FullName, emp.Email) == false)
                {
                    _responseDTO.Message = "Employee is already exists";
                    _responseDTO.IsSuccess = false;
                    return _responseDTO;
                }
                _haganContext.Employees.Add(emp);
                await _haganContext.SaveChangesAsync();
                var ids = await _haganContext.Employees.ToListAsync();
                var lastwork = ids.LastOrDefault();
                for (var i = 0; i < employee.TitleIds.Count; i++)
                {
                    Work work = new Work();
                    if (lastwork != null)
                        work.EmpId = lastwork.EmpId;
                    if (lastwork == null)
                        work.EmpId = 1;
                    work.TitleId = employee.TitleIds[i];
                    _haganContext.Work.Update(work);
                    await _haganContext.SaveChangesAsync();
                }
                _responseDTO.Result = "Employee Created";

            }
            catch (Exception ex)
            {
                _responseDTO.Message = ex.Message;
                _responseDTO.IsSuccess = false;
            }
            return _responseDTO;
        }
        public bool DoesNotExist(string Fullname, string Email, int id = 0)
        {
            // Check if any employee with the provided details exists
            bool exists = _haganContext.Employees.Any(ele =>
        ele.FullName == Fullname &&
        ele.Email == Email && ele.EmpId != id);
            // Return true if no matching employee is found
            return !exists;
        }
        public bool ExistTitle(EmployeeDto emp)
        {
            var titles = _haganContext.Titles.ToList();
            bool allInList = emp.TitleIds.All(x => titles.Any(ele => ele.TitleId == x));

            return allInList;
        }


        public async Task<ResponseDTO> UpdateItem(ItemDto2 itemDto)
        {
            var item = await _haganContext.Items.FirstOrDefaultAsync(e => e.Id == itemDto.Id);
            if (item == null)
            {

                _responseDTO.IsSuccess = false;
                _responseDTO.Result = "item isn't exist";
            }
            else
            {
                item.Health = itemDto.Health;
                item.CheckedDate = DateTime.Now;
                _haganContext.Update(item);
                await _haganContext.SaveChangesAsync();
                _responseDTO.Result = "Updated";
            }
            return _responseDTO;
        }
    }
}
