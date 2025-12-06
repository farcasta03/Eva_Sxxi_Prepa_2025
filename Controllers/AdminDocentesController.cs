using Microsoft.AspNetCore.Mvc;
using Eva_Sxxi_Prepa_2025.Data;
using Eva_Sxxi_Prepa_2025.Models;

namespace Eva_Sxxi_Prepa_2025.Controllers
{
    public class AdminDocentesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminDocentesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var docentes = _context.Docentes.ToList();
            return View(docentes);
        }


        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Docente docente)
        {
            docente.DocenteId = Guid.NewGuid().ToString();
            _context.Docentes.Add(docente);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Editar(string id)
        {
            var docente = _context.Docentes.Find(id);
            if (docente == null) return NotFound();
            return View(docente);
        }

        [HttpPost]
        public IActionResult Editar(Docente docente)
        {
            _context.Docentes.Update(docente);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Eliminar(string id)
        {
            var docente = _context.Docentes.FirstOrDefault(d => d.DocenteId == id);
            if (docente == null)
                return NotFound();

            _context.Docentes.Remove(docente);
            _context.SaveChanges();

            TempData["Mensaje"] = "Docente eliminado correctamente.";
            return RedirectToAction("Index");
        }





    }
}
