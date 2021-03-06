﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserAuth.Helpers;

namespace UserAuth.Data.Entities
{
  public class User
  {
    public User()
    {
     CreatedOnUtc = UpdatedOnUtc = AppConstants.MinDateForsql;
    }

    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailId { get; set; }
    public string Department { get; set; }
    public string Hospital { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public int EndoscopicSuite { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public ICollection<Room> Rooms { get; set; }
    public bool IsDisabled { get; set; }
    public DateTime UpdatedOnUtc { get; set; }
  }
}