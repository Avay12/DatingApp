using System;
using AutoMapper;
using DatingApp.Core.DTOs;
using DatingApp.Core.Entities;

namespace DatingApp.API.Helpers;

public class AutoMapperProfiles : Profile
{
  public AutoMapperProfiles()
  {
    CreateMap<AppUser, MemberDto>();
    CreateMap<Photo, PhotoDto>();
  }
}
