using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using DatingApp.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Infrastructure.Data;

public class Seed
{
  public static async Task SeedUsers(DataContext _context)
  {
    if (await _context.Users.AnyAsync()) return;

    var userData = await File.ReadAllTextAsync("../DatingApp.Infrastructure/Data/UserSeedData.json");

    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

    var users = JsonSerializer.Deserialize<List<AppUser>>(userData, options);

    if (users == null) return;

    foreach (var user in users)
    {
      using var hmac = new HMACSHA512();

      user.UserName = user.UserName.ToLower();
      user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));
      user.PasswordSalt = hmac.Key;

      _context.Users.Add(user);
    }

    await _context.SaveChangesAsync();
  }
}
