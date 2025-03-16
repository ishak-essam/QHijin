using System;
using System.Collections.Generic;

namespace BackEndHagan.Entities;

public  class Work
{
    public int Id { get; set; }
    public int TitleId { get; set; }
    public int EmpId { get; set; }
    public Title? Title { get; set; }
    public  Employee? Emp { get; set; }
}
