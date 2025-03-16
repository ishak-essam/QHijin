using System;
using System.Collections.Generic;

namespace BackEndHagan.Entities;

public partial class Contact
{
    public int ContactNo { get; set; }
    public string FullName { get; set; }
    public int UserId { get; set; }
    public string ContactMsg { get; set; }
    public string Email { get; set; }
    public string Subject { get; set; }
    public string Phone { get; set; }
    public DateTime ReadTime { get; set; }=DateTime.Now;
    public  User? User { get; set; }
}
