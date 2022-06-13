using FootballMatchesWebApp.Application.Interfaces;
using FootballMatchesWebApp.Application.Services;
using FootballMatchesWebApp.Data;
using FootballMatchesWebApp.Data.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace FootballMatchesWebApp.Extensions
{
    public static class ServiceCollectionExtenssions
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services

                .AddScoped<IRepository, Repository>()
                .AddScoped<ITeamService, TeamService>()
                .AddScoped<IFixtureService, FixtureService>();


            return services;
        }

        public static IServiceCollection AddApplicationDbContexts(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("FootballMatchesWebApp");
            services.AddDbContext<FootballMatchesDbContext>(options => options.UseSqlServer(connectionString));

            return services;
        }
    }
}
