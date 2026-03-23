using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lab.MVC.AppSemTemplate.Extensions
{
    [HtmlTargetElement("*", Attributes = "disable-by-claim-name")]
    [HtmlTargetElement("*", Attributes = "disable-by-claim-value")]
    public class DesabilitaLinkClaimTagHelper : TagHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public DesabilitaLinkClaimTagHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        [HtmlAttributeName("disable-by-claim-name")]
        public string? IdentityClaimName { get; set; }

        [HtmlAttributeName("disable-by-claim-value")]
        public string? IdentityClaimValue { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (output == null) throw new ArgumentNullException(nameof(output));

            if (_contextAccessor.HttpContext.User.Identity == null) throw new InvalidOperationException();

            var temAcesso = _contextAccessor.HttpContext.User.Identity.IsAuthenticated &&
                _contextAccessor.HttpContext.User.Claims.Any(c =>
                c.Type == IdentityClaimName &&
                c.Value.Split(',').Contains(IdentityClaimValue)
           );

            if (temAcesso) return;

            output.Attributes.RemoveAll("href");
            output.Attributes.Add("style", "cursor: not-allowed");
            output.Attributes.Add("title", "Você não tem permissão");
        }
    }
}
