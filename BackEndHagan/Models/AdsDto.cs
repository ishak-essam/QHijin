namespace QHijin.Models
{
    public class AdsDto
    {
        public int AdId { get; set; }
        public int EmpId { get; set; }
        public string Link { get; set; }
        public string? AdsImgUrl { get; set; }
        public string? AdsImgLocalPath { get; set; }
        public string AdsType { get; set; }
        public string Text { get; set; }
        public IFormFile? Image { get; set; }
    }
}
