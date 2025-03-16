namespace BackEndHagan.Models.Dto

{
    public class LoginRequestDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class AdminLoginRequestDTO
    {
        public int id { get; set; }
        public string Password { get; set; }
    }
}
