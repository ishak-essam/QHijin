using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
namespace BackEndHagan.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string FullName { get; set; }
        public string PassportNumber { get; set; }
        public bool IsActive { get; set; }=true;
    }
}
