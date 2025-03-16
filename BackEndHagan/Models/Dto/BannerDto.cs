using BackEndHagan.Entities;

namespace BackEndHagan.Models.Dto
{
    public class BannerDto
    {
        public int BanarNo { get; set; }
        public int EmpId { get; set; }
        public string SubTitleEn { get; set; }
        public string SubTitleAr { get; set; }
        public string ParphEn { get; set; }
        public string? ImgUrl { get; set; }
        public string? ImgLocalPath { get; set; }
        public string ParphAr { get; set; }
        public IFormFile? Image { get; set; }
    }
}
