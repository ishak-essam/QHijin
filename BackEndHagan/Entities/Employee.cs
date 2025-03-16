using NuGet.DependencyResolver;
using QHijin.Entities;
using System;
using System.Collections.Generic;

namespace BackEndHagan.Entities;

public partial class Employee
{
    public int EmpId { get; set; }
    public string FullName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public bool Status { get; set; } = true;
    public List<About> Abouts { get; set; } = new ();
    public virtual ICollection<Trainers> Trainers { get; set; }
    public List<Banner> Banners { get; set; } = new ();
    public List<EmployeeItem> EmployeeItems { get; set; } = new ();
    public List<PriceAndRate> PriceAndRates { get; set; } = new ();
    public List<Salary> Salaries { get; set; } = new ();
    public List<PrivacyAndPolicy> PrivacyAndPolicies { get; set; } = new ();
    public List<ServicesSite> Services { get; set; } = new (); 
    public List<Ads> Ads { get; set; } = new ();
    public List<ContactUs> ContactUs { get; set; } = new (); 
    public List<Advantages> Advantages { get; set; } = new(); 
    public List<Policy_Refund> Policy_Refund { get; set; } = new(); 
    public List<Contracting_Policy> Contracting_Policy { get; set; } = new();
    public List<Delivery_Period> Delivery_Period { get; set; } = new();
    public List<HowTobuy> HowTobuy { get; set; } = new();
    public List<SocialMedia> SocialMedia { get; set; } = new ();
    public List<TermsAndCondition> TermsAndConditions { get; set; } = new ();
    public List<Work> Works { get; set; } = new ();

}
