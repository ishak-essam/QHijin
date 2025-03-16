using System;
using System.Collections.Generic;

namespace BackEndHagan.Entities;

public partial class EmployeeItem
{
    public int EmpId { get; set; }

    public int ItemId { get; set; }

    public string? Doctor { get; set; }

    public virtual Employee Emp { get; set; } = null!;

    public virtual Item Item { get; set; } = null!;
}
