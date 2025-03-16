using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackEndHagan.Entities;

public  class Photo
{
    [Key]
    public int PhId { get; set; }
    public bool  IsMain { get; set; }
    public string? ImgUrl { get; set; }
    public string? ImgLocalPath { get; set; }
    public int  ItemId { get; set; }
    public  Item item { get; set; }
}
