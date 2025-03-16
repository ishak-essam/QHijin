using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEndHagan.Entities;

public  class Invoice
{
    public int InvoiceId { get; set; }
    public int UserId { get; set; }
    public int ItemId { get; set; }
    public string Type { get; set; }
    public DateTime DateTime { get; set; }
    public decimal Total { get; set; }
    public int InvoiceNo { get; set; }
    public  Item? Item { get; set; }
    public  User User { get; set; }

}
