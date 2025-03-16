using BackEndHagan.Entities;

namespace BackEndHagan.Models.Dto
{
    public class UserDTO2
    {
        public int Id { get; set; }

        public string FullName { get; set; }
        public string PassportNumber { get; set; }
        public string Phone { get; set; }

        public string Email { get; set; }
        public List<int> TypeId { get; set; } = [ ];
        public string Password { get; set; }
        public bool IsActive { get; set; } = true;

    }
}
