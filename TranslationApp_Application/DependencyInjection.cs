using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TranslationApp_Application.InterfaceService;
using TranslationApp_Application.Service;

namespace TranslationApp_Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<ITranslationProviderService, FunTranslationService>();
            return services;
        }
    }
}
