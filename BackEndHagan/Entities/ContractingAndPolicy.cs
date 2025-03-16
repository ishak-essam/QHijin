using BackEndHagan.Entities;
using System.ComponentModel.DataAnnotations;

namespace QHijin.Entities
{
    public class Contracting_Policy
    {
        [Key]
        public int con_pNo { get; set; }
        public int EmpId { get; set; }
        public string titleAr { get; set; }
        public string titleEn { get; set; }
        public string textAr { get; set; }
        public string textEn { get; set; }
        public Employee? Emp { get; set; }
    }
}
