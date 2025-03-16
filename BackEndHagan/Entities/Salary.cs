using System;
using System.Collections.Generic;

namespace BackEndHagan.Entities;

public partial class Salary
{
    public int SalaryNo { get; set; }

    public int EmpId { get; set; }

    public decimal? Money { get; set; }

    public DateTime? Date { get; set; }

    public  Employee? Emp { get; set; }
}
