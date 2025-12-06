using Microsoft.AspNetCore.Mvc;
using Eva_Sxxi_Prepa_2025.Data;
using Eva_Sxxi_Prepa_2025.Models;
using System.Linq;

namespace Eva_Sxxi_Prepa_2025.Controllers
{
    public class AdminAlumnosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminAlumnosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var alumnos = _context.Alumnos.ToList();
            return View(alumnos);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View(new Alumno
            {
                Matricula = "",
                Apellido1 = "",
                Apellido2 = "",
                Nombre = "",
                Semestre = "",
                Grupo = ""
            });
        }

        [HttpPost]
        public IActionResult Crear(Alumno alumno)
        {
            if (ModelState.IsValid)
            {
                // Agregar alumno
                _context.Alumnos.Add(alumno);

                // Verificar si ya existe usuario con esa matrícula
                var existeUsuario = _context.Usuarios.Any(u => u.UsuarioId == alumno.Matricula);
                if (!existeUsuario)
                {
                    var nuevoUsuario = new Usuario
                    {
                        UsuarioId = alumno.Matricula,
                        Contraseña = alumno.Apellido1,
                        Rol = "Alumno"
                    };

                    _context.Usuarios.Add(nuevoUsuario);
                }

                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(alumno);
        }

        [HttpGet]
        public IActionResult Editar(string id)
        {
            var alumno = _context.Alumnos.Find(id);
            if (alumno == null) return NotFound();

            return View(alumno);
        }

        [HttpPost]
        public IActionResult Editar(Alumno alumno)
        {
            if (ModelState.IsValid)
            {
                _context.Alumnos.Update(alumno);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(alumno);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Eliminar(string id)
        {
            var alumno = _context.Alumnos.Find(id);
            if (alumno == null) return NotFound();

            _context.Alumnos.Remove(alumno);

            // También eliminar al usuario vinculado
            var usuario = _context.Usuarios.FirstOrDefault(u => u.UsuarioId == id && u.Rol == "Alumno");
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
