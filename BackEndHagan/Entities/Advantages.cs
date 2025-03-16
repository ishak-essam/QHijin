using BackEndHagan.Entities;
using System.ComponentModel.DataAnnotations;

namespace QHijin.Entities
{
    public class Advantages
    {
        [Key]
        public int AdvId { get; set; }
        public int EmpId { get; set; }
        public string TitleAr { get; set; }
        public string TitleEn { get; set;}
        public string TextAr { get; set; }
        public string TextEn { get; set; }
        public Employee? Emp { get; set; }
    }
}
