using System;
using DatingApp.Core.DTOs;
using DatingApp.Core.Entities;

namespace DatingApp.Core.Interfaces;

public interface IUserRepository
{
  void update(AppUser user);
  Task<bool> SaveAllAsync();
  Task<IEnumerable<AppUser>> GetUsersAsync();
  Task<AppUser?> GetUserByIdAsync(int id);
  Task<AppUser?> GetUserByUsernameAsync(string username);
  Task<IEnumerable<MemberDto>> GetMembersAsync();
  Task<MemberDto?> GetMemberAsync(string username);

}
