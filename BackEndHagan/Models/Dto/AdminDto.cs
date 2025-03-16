using System;
using System.Collections.Generic;

namespace BackEndHagan.Models.Dto

{
    public class AdminDto
    {
        public int Id { get; set; }
        public int EmpId { get; set; }
        public string Password { get; set; }
        public int TitleId { get; set; } 
    }
}
