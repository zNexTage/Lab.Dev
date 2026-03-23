using Microsoft.AspNetCore.Mvc;

namespace Lab.MVC.ConhecimentoEssenciais.AppSemTemplate.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Site.Titulo"] = "Título legal";
            ViewData["Saudacoes"] = "Alo mundo";

            if (Request.Cookies.TryGetValue("MeuCookie", out string? cookieValue))
            {
                ViewData["MeuCookie"] = cookieValue;
            }

            return View();
        }

        [Route("cookies")]
        public IActionResult Cookie()
        {
            var opts = new CookieOptions()
            {
                Expires = DateTime.Now.AddHours(1),
            };

            Response.Cookies.Append("MeuCookie", "DATA", opts);
                       

            return View();
        }
    }
}
