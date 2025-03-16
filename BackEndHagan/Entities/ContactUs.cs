using BackEndHagan.Entities;
using System.ComponentModel.DataAnnotations;

namespace QHijin.Entities
{
    public class ContactUs
    {
        [Key]
        public int ContId { get; set; }
        public int EmpId { get; set; }
        public string TextAr { get; set; }
        public string TextEn { get; set; }
        public Employee? Emp { get; set; }
    }
}
