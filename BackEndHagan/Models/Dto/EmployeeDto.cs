using BackEndHagan.Entities;

namespace BackEndHagan.Models.Dto
{
    public class EmployeeDto
    {
        public int EmpId { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public bool Status { get; set; } = true;
        public string Email { get; set; }
        public List<int> TitleIds { get; set; } = [ ];
    }
}
