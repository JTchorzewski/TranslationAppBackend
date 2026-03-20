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
            services.AddHttpClient<ITranslationProviderService, FunTranslationService>(client =>
            {
                client.BaseAddress = new Uri("https://api.funtranslations.com/translate/");
                client.Timeout = TimeSpan.FromSeconds(10);
                client.DefaultRequestHeaders.Add("User-Agent", "MyTranslationApp/1.0");
            });
            services.AddScoped<ITranslationService, TranslationService>();
            services.AddScoped<ILogService, LogService>();

            return services;
        }
    }
}
