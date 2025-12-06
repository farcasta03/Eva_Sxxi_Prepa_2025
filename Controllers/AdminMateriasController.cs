using Microsoft.AspNetCore.Mvc;
using Eva_Sxxi_Prepa_2025.Data;
using Eva_Sxxi_Prepa_2025.Models;
using System.Linq;

namespace Eva_Sxxi_Prepa_2025.Controllers
{
    public class AdminMateriasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminMateriasController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var materias = _context.Materias.ToList();
            return View(materias);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View(new Materia { MateriaId = "", Nombre = "" });
        }



        [HttpPost]
        public IActionResult Crear(Materia materia)
        {
            if (ModelState.IsValid)
            {
                _context.Materias.Add(materia);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(materia);
        }

        [HttpGet]
        public IActionResult Editar(string id)
        {
            var materia = _context.Materias.Find(id);
            if (materia == null) return NotFound();

            return View(materia);
        }

        [HttpPost]
        public IActionResult Editar(Materia materia)
        {
            if (ModelState.IsValid)
            {
                _context.Materias.Update(materia);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(materia);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Eliminar(string id)
        {
            var materia = _context.Materias.Find(id);
            if (materia == null) return NotFound();

            _context.Materias.Remove(materia);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
