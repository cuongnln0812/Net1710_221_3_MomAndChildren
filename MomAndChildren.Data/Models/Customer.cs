﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace MomAndChildren.Data.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string Name { get; set; }

    public string Address { get; set; }

    public string PhoneNumber { get; set; }

    public DateTime Dob { get; set; }

    public int Gender { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}