using System;
using System.Collections.Generic;

namespace BackEndHagan.Entities;
public  class Title
{
    public int TitleId { get; set; }
    public string TitleName { get; set; }
    public bool IsAdmin { get; set; } = false;
    public List<Work>? Works { get; set; } = new ();
}
