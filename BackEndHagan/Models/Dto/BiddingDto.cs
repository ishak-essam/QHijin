using System;
using System.Collections.Generic;

namespace BackEndHagan.Models.Dto

{
    public class BiddingDto
    {
        public int BiddingNo { get; set; }

        public int UserId { get; set; }

        public string? Title { get; set; }

        public bool IsActive { get; set; } = true;
        public int ItemNo { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }
        public int Price { get; set; }
    }

}
