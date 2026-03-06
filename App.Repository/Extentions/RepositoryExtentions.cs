using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Repository.Extentions
{
    public static class RepositoryExtentions
    {
        public static IServiceCollection AddRepositoryExtentions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options => {
                var connectionTostring = configuration.GetSection(ConnectionToStringOption.Key).Get<ConnectionToStringOption>();
                options.UseSqlServer(connectionTostring!.SqlServer, sqlServerAction => {
                    sqlServerAction.MigrationsAssembly(typeof(RepositoryAssembly).Assembly.FullName);
                });
            
            });



            return services;
        }
    }
}
