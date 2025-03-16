using System;
using System.Collections.Generic;

namespace BackEndHagan.Entities;

public partial class SocialMedia
{
    public int SocialMedNo { get; set; }
    public int EmpId { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public string? ImgUrl { get; set; }
    public string? ImgLocalPath { get; set; }
    public  Employee? Emp { get; set; }
}
