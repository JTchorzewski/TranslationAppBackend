using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TranslationApp_Application.InterfaceRepository;
using TranslationApp_Infrastructure.Repository;

namespace TranslationApp_Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<ILogRepository, LogRepository>();
            return services;
        }
    }
}
