using BackEndHagan.Entities;
using System.Security.Claims;
namespace BackEndHagan.Extensions
{
    public static class ClaimsExtansions
    {
        public static string GetUsername ( this ClaimsPrincipal claimsPrincipal ) {
             return claimsPrincipal.FindFirst(ClaimTypes.Email)?.Value;
        }
        public static int GetUserId ( this ClaimsPrincipal claimsPrincipal )
        {
            return int.Parse(claimsPrincipal.FindFirst (ClaimTypes.NameIdentifier)?.Value);
        }
        public static List<int> GetRoles(this ClaimsPrincipal claimsPrincipal ) {
                 return (claimsPrincipal.Claims.Where (c => ( c.Type == "Role" )).Select (c => {
                     if ( c.Value != null || c.Value != "null" ) return int.Parse (c.Value);
                     else return 0  ;
                 }).ToList () );
        }
        public static int GetRole(this ClaimsPrincipal claimsPrincipal)
        {
            var roleClaim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "Role");

            if (roleClaim != null && !string.IsNullOrEmpty(roleClaim.Value) && roleClaim.Value != "null")
            {
                return int.Parse(roleClaim.Value);
            }

            return 0; // Return 0 if no valid role is found
        }

        public static string GetAdminRole ( this ClaimsPrincipal claimsPrincipal )
        {
            return claimsPrincipal.Claims.FirstOrDefault (ele=>ele.Type=="admin")?.Value.ToString()??"null";
        }
    }
}
