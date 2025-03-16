using Humanizer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackEndHagan.Entities;

public  class Bidding
{
    public int BiddingNo { get; set; }
    public int UserId { get; set; }
    public string? Title { get; set; }
    [Range (0, 3)]
    public int Status { get; set; }
    public DateTime StartDate { get; set; } = DateTime.Now;
    public DateTime EndDate { get; set; }
    public int Price { get; set; }
    public bool IsActive { get; set; } = true;
    public int ItemNo { get; set; }
    public Item Items { get; set; }
    public  User User { get; set; }
}

