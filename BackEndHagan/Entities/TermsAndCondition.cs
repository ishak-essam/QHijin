using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace BackEndHagan.Entities;

public partial class TermsAndCondition
{
    public int TcNo { get; set; }

    public int EmpId { get; set; }
    [MaxLength]

    public string TextEn { get; set; }
    [MaxLength]
    public string TextAr { get; set; }

    public  Employee? Emp { get; set; }
}
