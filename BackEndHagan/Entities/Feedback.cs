using BackEndHagan.Entities;
using System.ComponentModel.DataAnnotations;

namespace QHijin.Entities
{
    public class Feedback
    {
        [Key]
        public int FBId { get; set; }
        public int UserId { get; set; }
        public string email { get; set; }
        public string text { get; set; }
        public User? User { get; set; }
    }
}
