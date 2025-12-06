using Microsoft.AspNetCore.Mvc;
using Eva_Sxxi_Prepa_2025.Data;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Eva_Sxxi_Prepa_2025.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string matricula, string apellido1)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.UsuarioId == matricula && u.Contraseña == apellido1);

            if (usuario != null)
            {
                HttpContext.Session.SetString("UsuarioId", usuario.UsuarioId);
                HttpContext.Session.SetString("Rol", usuario.Rol);

                if (usuario.Rol == "Administrador")
                    return RedirectToAction("Index", "Admin");

                return RedirectToAction("Menu", "Evaluacion");
            }

            ViewBag.Mensaje = "Credenciales incorrectas";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}
