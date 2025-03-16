using BackEndHagan.Entities;

namespace BackEndHagan.Models.Dto
{
    public class SalaryDto
    {
        public int SalaryNo { get; set; }

        public int EmpId { get; set; }

        public decimal? Money { get; set; }

        public DateTime? Date { get; set; }
    }
}
