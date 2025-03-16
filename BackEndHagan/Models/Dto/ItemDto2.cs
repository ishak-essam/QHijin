namespace BackEndHagan.Models.Dto
{
    public class ItemDto2
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? BiddingNo { get; set; }
        public string Name { get; set; }
        public string History { get; set; }
        public bool saleStatus { get; set; } = false;
        public string? BaseUrl { get; set; }
        public double Price { get; set; }
        public string Type { get; set; }
        public string Currency { get; set; }
        public bool IsActive { get; set; } = true;
        public string? Disc { get; set; }
        public string? Health { get; set; }
        public DateTime CheckedDate { get; set; }
    }
}
