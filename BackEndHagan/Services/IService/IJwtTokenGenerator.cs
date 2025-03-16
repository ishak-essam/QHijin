
using BackEndHagan.Entities;
using BackEndHagan.Models;

namespace BackEndHagan.Services.IService

{
    public interface IJwtTokenGenerator
    {
        string GenerateTokenUser ( ApplicationUser applicationUser,int IdUser);
        string GenerateTokenDashboard ( Employee applicationUser,bool admin,string role);
    }
}
