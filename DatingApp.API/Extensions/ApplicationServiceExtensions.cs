using System;
using DatingApp.Core.Interfaces;
using DatingApp.Core.Services;
using DatingApp.Infrastructure.Data;
using DatingApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Extensions;

public static class ApplicationServiceExtensions
{
  public static IServiceCollection AddApplicationServices(this IServiceCollection services,
  IConfiguration config)
  {
    services.AddControllers();
    services.AddDbContext<DataContext>(opt =>
    {
      opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
    });
    services.AddCors();
    services.AddScoped<ITokenService, TokenService>();
    services.AddScoped<IUserRepository, UserRepository>();

    return services;
  }

}
