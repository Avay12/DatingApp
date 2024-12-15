using System;

namespace DatingApp.Core.Entities;

public class AppUser
{
  public int Id { get; set; }
  public required string UserName { get; set; }
}
