using QHijin.Entities;
using System;
using System.Collections.Generic;

namespace BackEndHagan.Entities;

public  class About
{
    public int AboutNo { get; set; }
    public int? Title { get; set; }
    public string TextEn { get; set; }
    public string TextAr { get; set; }
    public List<AboutPhotos> AboutPhoto { get; set; } = new();
    public int EmpId { get; set; }
    public  Employee? Emp { get; set; }
}
