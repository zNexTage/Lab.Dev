using Microsoft.AspNetCore.Mvc;

namespace Lab.MVC.AppSemTemplate.Controllers
{
    public class TesteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
