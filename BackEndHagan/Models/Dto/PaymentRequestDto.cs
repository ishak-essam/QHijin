using BackEndHagan.Entities;

namespace QHijin.Models.Dto
{
    public class PaymentRequestDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? TrainerId { get; set; }
        public int? ItemId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNo { get; set; }
        public string EmailAddress { get; set; }
    }
}
