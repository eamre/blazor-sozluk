﻿using BlazorSozluk.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Infrastructure.Persistence.Extensions
{
    public static class Registration
    {
        public static IServiceCollection AddInfrastructureRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BlazorSozlukContext>(options =>
            {
                var connStr = configuration["BlazorSozlukDbConnectionString"].ToString();
                options.UseNpgsql(connStr, opt =>
                {
                    opt.MigrationsHistoryTable("__EFMigrationsHistory", "dbo");
                    opt.EnableRetryOnFailure();
                });
            });

            //var seedData = new SeedData();
            //seedData.SeedAsync(configuration).GetAwaiter().GetResult();

            return services;
        }
    }
}