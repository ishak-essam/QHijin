using BackEndHagan.Entities;

namespace BackEndHagan.Models.Dto

{
    public class LoginResponseDTO
    {
        public User? User { get; set; }
        public EmployeeDto? Employee { get; set; }
        public string Token { get; set; }
    }
}
