using System;

namespace BackEndHagan.Models.Dto
{
    public class InvoiceDto
    {
        public int InvoiceId { get; set; }
        public string Type { get; set; }
        public int ItemId { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public decimal Total { get; set; }
        public int UserId { get; set; }
        public int InvoiceNo { get; set; }
    }

}
