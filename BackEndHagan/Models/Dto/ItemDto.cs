using QHijin.Models.Dto;

namespace BackEndHagan.Models.Dto
{
    public class ItemDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? BiddingNo { get; set; }
        public double Services { get; set; } = 2.5;
        public double VAT { get; set; } = 14;
        public string Name { get; set; }
        public string? BaseUrl { get; set; }
        public string History { get; set; }
        public bool saleStatus { get; set; } = false;
        public decimal? Price { get; set; }
        public string Currency { get; set; }
        public string Type { get; set; }
        public string? Disc { get; set; }
        public string? Health { get; set; }
        public ItemPhysicalDto ItemPhysical { get; set; }
        public DateTime CheckedDate { get; set; }
        public ICollection<IFormFile>? Images { get; set; }
        public IFormFile Video { get; set; }

    }
}
