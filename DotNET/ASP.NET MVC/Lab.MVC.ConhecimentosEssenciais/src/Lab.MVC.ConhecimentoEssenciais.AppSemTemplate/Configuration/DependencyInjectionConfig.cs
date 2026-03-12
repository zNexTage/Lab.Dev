using Lab.MVC.AppSemTemplate.Extensions;
using Microsoft.AspNetCore.Mvc.DataAnnotations;

namespace Lab.MVC.AppSemTemplate.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static WebApplicationBuilder AddDependencyInjectionConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IValidationAttributeAdapterProvider, MoedaValidationAttributeAdapterProvider>();

            return builder;
        }
    }
}
