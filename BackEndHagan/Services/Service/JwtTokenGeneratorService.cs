using BackEndHagan.Entities;
using BackEndHagan.Models;
using BackEndHagan.Services.IService;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BackEndHagan.Services.Service
{
    public class JwtTokenGeneratorService : IJwtTokenGenerator
    {

        private readonly JwtOptions _jwtOptions; 

        public JwtTokenGeneratorService(  IOptions< JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }
        public string GenerateTokenDashboard ( Employee applicationUser, bool admin ,string role )
        {
            var tokenhandler=new JwtSecurityTokenHandler();
            var key= Encoding.UTF8.GetBytes( _jwtOptions.Secret );
            var claimsList=new List<Claim>{
            new Claim(JwtRegisteredClaimNames.Email,applicationUser.Email),
            new Claim(JwtRegisteredClaimNames.NameId,applicationUser.EmpId.ToString()),
            new Claim("admin",admin.ToString()),
            new Claim("Role",role),
            new Claim(JwtRegisteredClaimNames.Name,(applicationUser.FullName).ToString()),
            };
            var tokenDescrptor=new SecurityTokenDescriptor
            {
                Audience=_jwtOptions.Audience,
                Issuer=_jwtOptions.Issuer,
                Subject= new ClaimsIdentity(claimsList),
                Expires =DateTime.Now.AddDays(7),
                SigningCredentials=new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
            };
            var token =tokenhandler.CreateToken(tokenDescrptor);
            return tokenhandler.WriteToken (token);
        }

        public string GenerateTokenUser ( ApplicationUser applicationUser,int IdUser )
        {
            var tokenhandler=new JwtSecurityTokenHandler();
            var key= Encoding.UTF8.GetBytes( _jwtOptions.Secret );
            var claimsList=new List<Claim>{
            new Claim(JwtRegisteredClaimNames.Email,applicationUser!.Email!),
            new Claim(JwtRegisteredClaimNames.NameId,IdUser.ToString()),
            new Claim(JwtRegisteredClaimNames.Name,applicationUser!.UserName!.ToString()),
             new Claim("Role","0")
            };
            //claimsList.AddRange(roles.Select(ele=>new Claim(ClaimTypes.Role,ele)));
            var tokenDescrptor=new SecurityTokenDescriptor{
                Audience=_jwtOptions.Audience,
                Issuer=_jwtOptions.Issuer,
                Subject= new ClaimsIdentity(claimsList),
                Expires =DateTime.Now.AddDays(7),
                SigningCredentials=new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
            };
            var token =tokenhandler.CreateToken(tokenDescrptor);
            return tokenhandler.WriteToken (token);
        }
    }
}




//var tokenhandler=new JwtSecurityTokenHandler();
//var key= Encoding.UTF8.GetBytes( _jwtOptions.Secret );
//var claimsList=new []{
//            new Claim(JwtRegisteredClaimNames.Email,applicationUser.Email),
//            new Claim(JwtRegisteredClaimNames.Sub,applicationUser.Id),
//            new Claim(ClaimTypes.Role,roles.FirstOrDefault()),
//            new Claim(JwtRegisteredClaimNames.Name,applicationUser.UserName.ToString()),
//            };
////claimsList.AddRange(roles.Select(ele=>new Claim(ClaimTypes.Role,ele)));
//var tokenDescrptor=new SecurityTokenDescriptor
//{
//    Audience=_jwtOptions.Audience,
//    Issuer=_jwtOptions.Issuer,
//    Claims= claimsList,
//    Expires =DateTime.Now.AddDays(7),
//    SigningCredentials=new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256)
//};
//var token =tokenhandler.CreateToken(tokenDescrptor);
//return tokenhandler.WriteToken (token);