using System;
using DatingApp.Core.Entities;
using DatingApp.Core.Interfaces;
using DatingApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Infrastructure.Repositories;

public class UserRepository(DataContext _context) : IUserRepository
{
  public async Task<AppUser?> GetUserByIdAsync(int id)
  {
    return await _context.Users.FindAsync(id);
  }

  public async Task<AppUser?> GetUserByUsernameAsync(string username)
  {
    return await _context.Users.SingleOrDefaultAsync(x => x.UserName == username);
  }

  public async Task<IEnumerable<AppUser>> GetUsersAsync()
  {
    return await _context.Users.ToListAsync();
  }

  public async Task<bool> SaveAllAsync()
  {
    return await _context.SaveChangesAsync() > 0;
  }

  public void update(AppUser user)
  {
    _context.Entry(user).State = EntityState.Modified;
  }
}
