using BackEndHagan.Entities;

namespace BackEndHagan.Models.Dto
{
    public class RegistrationRequestDTO
    {
        public int Id { get; set; }

        public string FName { get; set; }

        public string LName { get; set; }

        public string Phone { get; set; }

        public string? Email { get; set; }

        public string Type { get; set; }

        public string Password { get; set; }

        public virtual ICollection<Bidding> Biddings { get; set; } = new List<Bidding> ();

        public virtual ICollection<InvoiceDto> Invoices { get; set; } = new List<InvoiceDto> ();

        public virtual ICollection<Item> Items { get; set; } = new List<Item> ();

        public virtual ICollection<Contact> Contact { get; set; } = new List<Contact> ();
    }
}
