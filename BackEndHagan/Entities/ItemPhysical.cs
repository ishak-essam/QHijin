using BackEndHagan.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QHijin.Entities
{
    public class ItemPhysical
    {
        [Key]
        public int Id { get; set; }        
        public string body { get; set; }
        public string eye { get; set; }
        public string foot { get; set; }
        public string front { get; set; }
        public string back { get; set; }
        public  int ItemId { get; set; }
        public virtual Item Item { get; set; }
    }
}
