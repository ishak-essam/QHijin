using BackEndHagan.Entities;
using System.ComponentModel.DataAnnotations;

namespace QHijin.Entities
{
    public class Ads
    {
        [Key]
        public int AdId  { get; set; }
        public string Link { get; set; }
        public string? AdsImgUrl { get; set; }
        public string? AdsImgLocalPath { get; set; }
        public string AdsType { get; set; }
        public string Text { get; set; }
        public int EmpId { get; set; }
        public Employee? Emp { get; set; }
    }
}
