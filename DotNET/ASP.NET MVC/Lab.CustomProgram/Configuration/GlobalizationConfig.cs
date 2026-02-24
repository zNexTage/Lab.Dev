using System.Globalization;

namespace Lab.CustomProgram.Configuration
{
    public static class GlobalizationConfig
    {
        public static WebApplication UseGlobalizationConfig(this WebApplication app) {
            var defaultCulture = new CultureInfo("pt-BR");

            var localization = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(defaultCulture),
                SupportedCultures = new[] { defaultCulture },
                SupportedUICultures = new[] { defaultCulture }
            };

            // Define a cultura e não deixa o browser definir.
            app.UseRequestLocalization(localization);
            
            return app;
        }
    }
}
