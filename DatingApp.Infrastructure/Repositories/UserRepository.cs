using System;
using System.IO.Compression;
using System.Security.Claims;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DatingApp.Core.DTOs;
using DatingApp.Core.Entities;
using DatingApp.Core.Interfaces;
using DatingApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Infrastructure.Repositories;

public class UserRepository(DataContext _context, IMapper mapper) : IUserRepository
{

  public async Task<MemberDto?> GetMemberAsync(string username)
  {
    return await _context.Users
        .Where(x => x.UserName == username)
        .ProjectTo<MemberDto>(mapper.ConfigurationProvider)
        .SingleOrDefaultAsync();
  }

  public async Task<IEnumerable<MemberDto>> GetMembersAsync()
  {
    return await _context.Users
          .ProjectTo<MemberDto>(mapper.ConfigurationProvider)
          .ToListAsync();
  }

  public Task<IEnumerable<MemberDto>> GetMembersAsync(string username)
  {
    throw new NotImplementedException();
  }

  public async Task<AppUser?> GetUserByIdAsync(int id)
  {
    return await _context.Users.FindAsync(id);
  }

  public async Task<AppUser?> GetUserByUsernameAsync(string username)
  {
    return await _context.Users
          .Include(x => x.Photos)
          .SingleOrDefaultAsync(x => x.UserName == username);
  }

  public async Task<IEnumerable<AppUser>> GetUsersAsync()
  {
    return await _context.Users
          .Include(x => x.Photos)
          .ToListAsync();
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
