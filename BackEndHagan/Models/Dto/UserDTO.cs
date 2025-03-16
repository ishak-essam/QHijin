using BackEndHagan.Entities;

namespace BackEndHagan.Models.Dto
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public List<int> TypeId { get; set; }
        public bool IsActive { get; set; } = true;
        public string Password { get; set; }
    }
}
