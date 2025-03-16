using BackEndHagan.Entities;

namespace QHijin.Models.Dto
{
    public class ItemPhysicalDto
    {
        public int Id { get; set; }
        public string body { get; set; }
        public string eye { get; set; }
        public string foot { get; set; }
        public string front { get; set; }
        public string back { get; set; }
        public int ItemId { get; set; }
    }
}
