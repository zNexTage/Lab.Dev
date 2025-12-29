using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lab.MVC.AppSemTemplate.Extensions
{
    [HtmlTargetElement("*", Attributes = "tipo-botao, route-id")] // Para qual elemento vou usar esse helper
    public class BotaoTagHelper : TagHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly LinkGenerator _linkGenerator;

        public BotaoTagHelper(IHttpContextAccessor contextAccessor, LinkGenerator link)
        {
            _contextAccessor = contextAccessor;
            _linkGenerator = link;
        }


        [HtmlAttributeName("tipo-botao")]
        public TipoBotao TipoBotaoSelecao { get; set; }

        [HtmlAttributeName("route-id")]
        public string RouteId { get; set; }



        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string nomeAction = string.Empty;
            string nomeClasse = string.Empty;
            string spanIcone = string.Empty;

            switch (TipoBotaoSelecao)
            {
                case TipoBotao.Detalhes:
                    {
                        nomeAction = "Details";
                        nomeClasse = "btn btn-info";
                        spanIcone = "fa fa-search";
                        break;
                    }
                case TipoBotao.Editar:
                    {
                        nomeAction = "Edit";
                        nomeClasse = "btn btn-warning";
                        spanIcone = "fa fa-pencil-alt";
                        break;
                    }
                case TipoBotao.Excluir:
                    {
                        nomeAction = "Delete";
                        nomeClasse = "btn btn-danger";
                        spanIcone = "fa fa-trash";
                        break;
                    }
            }

            var controller = _contextAccessor.HttpContext?.GetRouteData().Values["controller"]?.ToString();
            var host = $"{_contextAccessor.HttpContext.Request.Scheme}://{_contextAccessor.HttpContext.Request.Host.Value}";

            var caminho = _linkGenerator.GetPathByAction(
                _contextAccessor.HttpContext,
                nomeAction,
                controller,
                values: new { id = RouteId }
            );

            output.TagName = "a";
            output.Attributes.SetAttribute("href", $"{host}{caminho}");
            output.Attributes.SetAttribute("class", nomeClasse);

            var iconSpan = new TagBuilder("span");
            iconSpan.AddCssClass(spanIcone);

            output.Content.AppendHtml(iconSpan);
        }
    }

    public enum TipoBotao
    {
        Detalhes = 1,
        Editar,
        Excluir
    }
}
