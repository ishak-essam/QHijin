using BackEndHagan.Entities;
using System.ComponentModel.DataAnnotations;

namespace QHijin.Entities
{
    public class ServicesSite
    {
        [Key]
        public int SerId { get; set; }
        public int EmpId { get; set; }
        public string ParphAr { get; set; }
        public string ParphEn { get; set; }
        public string SubTitleEn { get; set; }
        public string SubTitleAr { get; set; }
        public Employee? Emp { get; set; }
    }
}
