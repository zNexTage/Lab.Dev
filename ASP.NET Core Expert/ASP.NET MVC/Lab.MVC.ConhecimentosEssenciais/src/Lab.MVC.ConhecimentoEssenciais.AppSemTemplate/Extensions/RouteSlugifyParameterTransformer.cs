using System.Text.RegularExpressions;

namespace Lab.MVC.AppSemTemplate.Extensions
{
    public class RouteSlugifyParameterTransformer : IOutboundParameterTransformer
    {
        public string? TransformOutbound(object? value)
        {
            if(value is null) return string.Empty;

            return Regex.Replace(value.ToString()!, "([a-z])([A-Z])",
                "$1-$2",
                RegexOptions.CultureInvariant,
                TimeSpan.FromMilliseconds(100))
                .ToLowerInvariant();               
        }
    }
}
