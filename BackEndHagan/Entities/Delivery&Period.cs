using BackEndHagan.Entities;
using System.ComponentModel.DataAnnotations;

namespace QHijin.Entities
{
    public class Delivery_Period
    {
        [Key]
        public int d_pN { get; set; }
        public int EmpId { get; set; }
        public string titleAr { get; set; }
        public string titleEn { get; set; }
        public string textAr { get; set; }
        public string textEn { get; set; }
        public Employee? Emp { get; set; }
    }
}
