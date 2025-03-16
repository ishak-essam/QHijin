


using QHijin.Entities;
namespace BackEndHagan.Entities;
public  class User
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string PassportNumber { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
    public bool IsActive { get; set; } = true;
    public List<QHijin.Entities.Action> Actions { get; set; } = new ();
    public List<Invoice> Invoices { get; set; } = new ();
    public List<Bidding> Biddings { get; set; } = new ();
    public List<Item> Items { get; set; } = new ();

    public List<Feedback> Feedback { get; set; } = new();
    public virtual ICollection<PaymentRequest> PaymentRequest { get; set; }

    public List<Contact> Contacts { get; set; } = new ();
}
