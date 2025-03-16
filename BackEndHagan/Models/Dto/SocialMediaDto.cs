

namespace BackEndHagan.Models.Dto
{
    public class SocialMediaDto
    {
        public int SocialMedNo { get; set; }
        public int EmpId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public IFormFile? Image { get; set; }
    }
}
