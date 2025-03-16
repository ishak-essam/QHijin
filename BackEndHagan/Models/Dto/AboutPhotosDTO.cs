using BackEndHagan.Entities;

namespace QHijin.Models.Dto
{
    public class AboutPhotosDTO
    {
        public int AbPhId { get; set; }
        public ICollection<IFormFile> Images { get; set; }
        public int AboutId { get; set; }
    }
    public class AboutPhotoDTO
    {
        public int AbPhId { get; set; }
        public IFormFile Image { get; set; }
        public int AboutId { get; set; }
    }
}
