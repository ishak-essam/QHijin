
using NuGet.DependencyResolver;
using QHijin.Entities;
using System.ComponentModel.DataAnnotations;

namespace BackEndHagan.Entities;

public  class Item
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int? BiddingNo { get; set; }
    public string Name { get; set; }
    public string History { get; set; }
    public double Price { get; set; }
    public string? Disc { get; set; }
    public virtual ICollection<Trainers> Trainers { get; set; }
    public bool IsActive { get; set; } = true;
    public bool saleStatus { get; set; } = false;
    public string? Health { get; set; }
    public string VideoUrl { get; set; }
    [Range(0, 100, ErrorMessage = "The value  must be between 0 and 100.")]
    public double Services { get; set; } = 2.5;
    [Range(0, 100, ErrorMessage = "The value must be between 0 and 100.")]
    public double VAT { get; set; } = 10;
    public string VideoLocalPath { get; set; }
    public DateTime CheckedDate { get; set; } 
    public string Currency { get; set; }
    public string Type { get; set; }
    public  Bidding? BiddingNoNavigation { get; set; }
    public  User User { get; set; }
    public virtual ICollection<PaymentRequest> PaymentRequest { get; set; } 
    public virtual ItemPhysical ItemPhysical { get; set; }
    public List<Photo> Photo { get; set; } = new ();
    public List<EmployeeItem> EmployeeItems { get; set; } = new ();
    public List<Invoice> Invoices { get; set; } = new ();
}
