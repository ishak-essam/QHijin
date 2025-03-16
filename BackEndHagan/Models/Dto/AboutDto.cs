using System;
using System.Collections.Generic;

namespace BackEndHagan.Models.Dto
{
    public class AboutDto
    {
        public int AboutNo { get; set; }
        public int? Title { get; set; }
        public string TextEn { get; set; }
        public string TextAr { get; set; }
        public int EmpId { get; set; }
    }
}

