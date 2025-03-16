
namespace BackEndHagan.Models.Dto
{
    public class TitleDto
    {
        public int TitleId { get; set; }
        public string TitleName { get; set; }
        public bool IsAdmin { get; set; } = false;
    }
}