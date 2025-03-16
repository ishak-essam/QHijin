using BackEndHagan.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QHijin.Entities
{
    public class Trainers
    {
        [Key]
        public int trnId { get; set; }
        public int? itemId { get; set; }
        public int empId { get; set; }
        public string trnImgLocalPath { get; set; }
        public bool status { get; set; } = true;
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string trnImgUrl { get; set; }
        public string trnCV { get; set; }
        [Range(0, 100, ErrorMessage = "The value for {0} must be between {1} and {2}.")]
        public double Services { get; set; } = 2.5;
        [Range(0, 100, ErrorMessage = "The value for {0} must be between {1} and {2}.")]
        public double VAT { get; set; } = 10;
        public string rpayment_link { get; set; }
        public double rentmoney { get; set; }
        public Employee? Employee { get; set; }
        public Item? Item { get; set; }
        public virtual ICollection<PaymentRequest> PaymentRequest { get; set; }
    }
}
