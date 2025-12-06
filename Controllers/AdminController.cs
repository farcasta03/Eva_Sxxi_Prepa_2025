using Microsoft.AspNetCore.Mvc;

namespace Eva_Sxxi_Prepa_2025.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Rol") != "Administrador")
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }
    }
}
