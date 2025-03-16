using BackEndHagan.Entities;
using System.ComponentModel.DataAnnotations;

namespace QHijin.Entities
{
    public class AboutPhotos
    {
        [Key]
        public int AbPhId { get; set; }
        public string? ImgUrl { get; set; }
        public string? ImgLocalPath { get; set; }
        public int AboutId { get; set; }
        public About About { get; set; }
    }
}
