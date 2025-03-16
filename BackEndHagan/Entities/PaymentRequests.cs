using BackEndHagan.Entities;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace QHijin.Entities
{
    public class PaymentRequest
    {
        [Key]
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? TrainerId { get; set; }
        public int ItemId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNo { get; set; }
        public string EmailAddress { get; set; }
        public string CountryName { get; set; }
        public double PaymentAmount { get; set; }
        public string PaymentCurrency { get; set; }
        public bool PaymentDone { get; set; } = false;
        public DateTime? PaymentDate { get; set; }
        public DateTime SendDate { get; set; }=DateTime.Now;
        public string OrderDetails { get; set; }
        public string RequestRef { get; set; }
        public User User { get; set; }
        public Item Item { get; set; }
        public Trainers Trainer { get; set; }
    }
}
