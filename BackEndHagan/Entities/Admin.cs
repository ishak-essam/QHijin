using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackEndHagan.Entities;

public  class Admin
{
    public int Id { get; set; }
    public int EmpId { get; set; }
    public string Email { get; set; }
    public byte [ ] PasswordHash { get; set; }
    public byte [ ] PasswordSalt { get; set; }
    public  Employee? Emp { get; set; }
    public int TitleId { get; set; } 
    public Title Titles { get; set; } 
}
