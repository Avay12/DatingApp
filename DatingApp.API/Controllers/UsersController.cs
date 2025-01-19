using System.Security.Claims;
using AutoMapper;
using DatingApp.Core.DTOs;
using DatingApp.Core.Entities;
using DatingApp.Core.Interfaces;
using DatingApp.Infrastructure.Data;
using DatingApp.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Controllers
{
    [Authorize]
    public class UsersController(IUserRepository _userRepository, IMapper mapper) : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            var users = await _userRepository.GetMembersAsync();

            return Ok(users);
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            var user = await _userRepository.GetMemberAsync(username);

            if (user == null) return NotFound();

            return user;
        }

        [HttpPut]
        public async Task<ActionResult> UpdatedUser(MemberUpdateDto memberUpdateDto)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (username == null) return BadRequest("No username found in token");

            var user = await _userRepository.GetUserByUsernameAsync(username);

            if (user == null) return BadRequest("Could not find user");

            mapper.Map(memberUpdateDto, user);

            if (await _userRepository.SaveAllAsync()) return NoContent();
            return BadRequest("Failed to update the user");
        }
    }
}
