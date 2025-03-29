using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
// using Repository.Repositories.Links;
// using Repository.Repositories.Accesses;
using Repository.Repositories.Users;
using Application.Contexts.Links.Repositories;
using Application.Contexts.Accesses.Repositories;
using Application.Contexts.Users.Repositories;

namespace IoC.Repositories;

public static class BuilderMapster
{
     public static WebApplicationBuilder AddRepositoriesConf(this WebApplicationBuilder builder)
     {
        builder.Services.AddScoped<IUserRepository, UserRepository>();
      //   builder.Services.AddScoped<ILinkRepository, LinkRepository>();
      //   builder.Services.AddScoped<IAccessRepository, AccessRepository>();

        return builder;
     }
}