using CleanArchitecture.Application.Contracts.Infrastucture;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Models;
using CleanArchitecture.Domain;
using CleanArchitecture.Infrastucture.Email;
using CleanArchitecture.Infrastucture.Persistence;
using CleanArchitecture.Infrastucture.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastucture
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastuctureServices(this IServiceCollection services, IConfiguration configurations)
        {
            services.AddDbContext<StreamerDbContext>(x => x.UseSqlServer(configurations.GetConnectionString("ConnectionString")));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositorysBase<>));
            services.AddScoped<IVideoRepository, VideoRepository>();
            services.AddScoped<IStreamerRepository, StreamerRepository>();
            services.Configure<EmailSettings>(x => configurations.GetSection("EmailSettings"));
            services.AddTransient<IEmailService, EmailService>();
            

            return services;
        }
    }
}
