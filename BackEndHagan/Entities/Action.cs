using BackEndHagan.Entities;

namespace QHijin.Entities
{
    public class Action
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public int UserId { get; set; }
        public BackEndHagan.Entities.Type? Type { get; set; }
        public User? User { get; set; }
    }
}
