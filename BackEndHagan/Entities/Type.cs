using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackEndHagan.Entities;

public partial class Type
{
    [Key]
    public int TypeId { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; } = true;
    public List<QHijin.Entities.Action> Actions { get; set; } = new ();
}
