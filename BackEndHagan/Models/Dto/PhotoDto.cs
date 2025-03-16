
namespace BackEndHagan.Models.Dto
{
    public class PhotoDto
    {
        public int PhId { get; set; }
        public int ItemId { get; set; }
        public bool IsMain { get; set; }
        public string? ImgUrl { get; set; }
        public string? ImgLocalPath { get; set; }
        public IFormFile? Image { get; set; }
    }
}
