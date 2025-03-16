using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackEndHagan.Entities;

public  class PrivacyAndPolicy
{
    public int PpNo { get; set; }

    public int EmpId { get; set; }

    [MaxLength]
    public string? TextEn { get; set; }
    [MaxLength]
    public string? TextAr { get; set; }

    public  Employee? Emp { get; set; }
}
