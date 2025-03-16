using BackEndHagan.Entities;
using BackEndHagan.Models;
using BackEndHagan.Models.Dto;
using BackEndHagan.Services.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace BackEndHagan.Services.Service
{
    public class UserService : IUserService
    {
        private  HaganContext _haganContext;
        private  ResponseDTO _responseDTO;
        public UserService ( HaganContext haganContext,
            UserManager<ApplicationUser> userManager )
        {
            _haganContext = haganContext;
            _responseDTO = new ResponseDTO ();
        }
        public async Task<ResponseDTO> GetUserById(int Userid)
        {
            try
            {
                var user = await _haganContext.Users.Include(ele => ele.Actions).ThenInclude(e => e.Type)
                    .Include(ele => ele.Invoices)
                    .Include(ele => ele.Contacts)
                    .Include(e => e.Biddings)
        .Include(e => e.Items)
        .ThenInclude(item => item.Photo)
        .Include(ele => ele.Biddings)
                    .FirstOrDefaultAsync(x => x.Id == Userid);
                if (user != null)
                {
                    _responseDTO.Result = user;
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

        public async Task<ResponseDTO> GetUserByName ( string name = "" )
        {
            try
            {
                IQueryable<User> query =_haganContext.Users;
                if ( query != null )
                {
                    query = query.Where (x => ( x.FullName ).Contains (name));
                    _responseDTO.Result = await query.ToListAsync();
                }
                return _responseDTO;
            }
            catch ( Exception ex )
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message.ToString ();
            }
            return _responseDTO;
        }

        public async Task<ResponseDTO> GetUserByPhone ( string phone = "" )
        {
            try
            {
                IQueryable<User> query =_haganContext.Users;
                if ( query != null )
                {
                    query = query.Where (x => ( x.Phone ).Contains (phone));
                    _responseDTO.Result = await query.ToListAsync();
                }
            }
            catch ( Exception ex )
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message.ToString ();
            }
            return _responseDTO;
        }

        public async Task<ResponseDTO> GetUsers ( )
        {
            var items = await _haganContext.Users.Include(e=>e.Biddings)
                        .Include(e => e.Items)
        .ThenInclude(item => item.Photo).Include(E=>E.Actions).ThenInclude(a=>a.Type).ToListAsync();
             _responseDTO.Result = items;
            return _responseDTO;
        }

        public async Task<ResponseDTO> RemoveUser (int Id )
        {
            try
            {
                //var httpContext = _httpContextAccessor.HttpContext;
                //var Id=httpContext.User.GetUserId();
                var user = await  _haganContext.Users.FirstOrDefaultAsync(u=>u.Id==Id);
                var userApp = await _haganContext.ApplicationUsers.FirstOrDefaultAsync(u=>u.Email == user!.Email!);
                if ( user != null )
                {
                    user.IsActive = false;
                    userApp!.IsActive = false;
                    _haganContext.Users.Update (user);
                    await _haganContext.SaveChangesAsync ();
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
        public async Task<ResponseDTO> RenewUser ( int Id )
        {
            try
            {
                //var httpContext = _httpContextAccessor.HttpContext;
                //var Id=httpContext.User.GetUserId();
                var user = await _haganContext.Users.FirstOrDefaultAsync(u=>u.Id==Id);
                var userApp = await  _haganContext.ApplicationUsers.FirstOrDefaultAsync(u=>u.Email == user!.Email);
                if ( user != null )
                {
                    user.IsActive = true;
                    userApp!.IsActive = true;
                    _haganContext.Users.Update (user);
                    _haganContext.SaveChanges ();
                    _responseDTO.Result = "Renew";
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

        public async Task<ResponseDTO> UpdateUser (  UserDTO2 UserDto )
        {
            try
            {
                var user = await _haganContext.Users.Include(e=>e.Actions).FirstOrDefaultAsync(u => u.Id == UserDto.Id);
                var userApp = await _haganContext.ApplicationUsers.FirstOrDefaultAsync(u => user!.Email == user.Email);
                var userType = ExistType (UserDto);
                if ( userType == false )
                {
                    _responseDTO.Message = "Type not found";
                    _responseDTO.IsSuccess = false;
                }
                if ( user != null && userApp != null  )
                {
                    user.FullName = UserDto.FullName;
                    List<QHijin.Entities.Action> Actions=user.Actions;
                    var count=Actions.Count>UserDto.TypeId.Count?Actions.Count:UserDto.TypeId.Count;
                    var x=UserDto.TypeId.Count;
                    var y=Actions.Count ;
                    for ( var i = 0; i < count; i++ )
                    {

                        if ( ( y > i && x > i ) )
                        {
                            Actions [ i ].TypeId = UserDto.TypeId [ i ];
                            _haganContext.Actions.Update (Actions [ i ]);
                        }
                        else if ( x > i && y < x )
                        {
                            await _haganContext.Actions.AddAsync (new QHijin.Entities.Action
                            {
                                Id = 0,
                                UserId = user.Id,
                                TypeId = UserDto.TypeId [ i ],
                            });
                        }
                        else if ( y > i && y > x )
                        {
                            _haganContext.Actions.Remove (Actions [ i ]);
                        }

                        await _haganContext.SaveChangesAsync ();
                    }
                    user.Email = UserDto.Email;
                    user.Phone = UserDto.Phone;
                    userApp.Email = UserDto.Email;
                    userApp.UserName = UserDto.FullName;
                    userApp.PhoneNumber = UserDto.Phone;
                    user.Invoices = [];
                    user.Biddings = [];
                    user.Items = [];
                    user.Contacts = [];
                    _haganContext.Users.Update (user);
                    await _haganContext.SaveChangesAsync ();
                    _responseDTO.Result = "Updated";
                }
                else
                {
                    _responseDTO.Message = "User not found";
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
        public  bool ExistType ( UserDTO2 emp )
        {
            var titles=_haganContext.Titles.ToList();
            bool allInList = emp.TypeId.All(x => titles.Any(ele => ele.TitleId == x));
            return allInList;
        }

    }
}
