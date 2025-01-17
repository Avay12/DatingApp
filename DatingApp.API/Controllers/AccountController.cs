using System;
using System.Security.Cryptography;
using System.Text;
using DatingApp.Core.DTOs;
using DatingApp.Core.Entities;
using DatingApp.Core.Interfaces;
using DatingApp.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Controllers;

public class AccountController(DataContext _context, ITokenService _tokenService) : BaseApiController
{
  [HttpPost("register")] //acount/register
  public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
  {
    if (await UserExists(registerDto.Username)) return BadRequest("Username is taken");

    return Ok();
    // using var hmac = new HMACSHA512();

    // var user = new AppUser
    // {
    //   UserName = registerDto.Username.ToLower(),
    //   PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
    //   PasswordSalt = hmac.Key
    // };

    // _context.Users.Add(user);
    // await _context.SaveChangesAsync();

    // return new UserDto
    // {
    //   Username = user.UserName,
    //   Token = _tokenService.CreateToken(user)
    // };
  }

  [HttpPost("login")]
  public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
  {
    var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());

    if (user == null) return Unauthorized("Invalid username");

    using var hamc = new HMACSHA512(user.PasswordSalt);

    var computedHash = hamc.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

    for (int i = 0; i < computedHash.Length; i++)
    {
      if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
    }

    return new UserDto
    {
      Username = user.UserName,
      Token = _tokenService.CreateToken(user)
    };
  }


  private async Task<bool> UserExists(string Username)
  {
    return await _context.Users.AnyAsync(x => x.UserName.ToLower() == Username.ToLower()); // Daniel != Daniel
  }
}
