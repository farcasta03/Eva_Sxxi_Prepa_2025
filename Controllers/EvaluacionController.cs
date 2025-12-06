using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Eva_Sxxi_Prepa_2025.Data;
using System.Linq;
using Eva_Sxxi_Prepa_2025.Models;

namespace Eva_Sxxi_Prepa_2025.Controllers
{
    public class EvaluacionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EvaluacionController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Menu()
        {
            var usuarioId = HttpContext.Session.GetString("UsuarioId");
            var rol = HttpContext.Session.GetString("Rol");

            if (string.IsNullOrEmpty(usuarioId) || rol != "Alumno")
                return RedirectToAction("Index", "Login");

            var alumno = _context.Alumnos.FirstOrDefault(a => a.Matricula == usuarioId);
            ViewBag.Alumno = alumno;

            return View();
        }

        public IActionResult EvaluarDocente()
        {
            var usuarioId = HttpContext.Session.GetString("UsuarioId");
            var alumno = _context.Alumnos.FirstOrDefault(a => a.Matricula == usuarioId);
            if (alumno == null) return RedirectToAction("Index", "Login");

            // Convertir a int de forma segura
            int semestreAlumno = int.TryParse(alumno.Semestre, out var s) ? s : -1;
            int grupoAlumno = int.TryParse(alumno.Grupo, out var g) ? g : -1;

            if (semestreAlumno == -1 || grupoAlumno == -1)
            {
                return BadRequest("Error al interpretar el semestre o grupo del alumno.");
            }

            var docentes = _context.DocenteMaterias
                .Where(dm => dm.Semestre == alumno.Semestre.ToString() && dm.Grupo == alumno.Grupo.ToString())
                .Join(_context.Docentes,
                      dm => dm.DocenteId,
                      d => d.DocenteId,
                      (dm, d) => new { dm.Id, d.NombreCompleto, dm.MateriaId })
                .Join(_context.Materias,
                      x => x.MateriaId,
                      m => m.MateriaId,
                      (x, m) => new
                      {
                          DocenteMateriaId = x.Id,
                          NombreDocente = x.NombreCompleto,
                          NombreMateria = m.Nombre
                      })
                .ToList();

            ViewBag.Docentes = docentes;
            ViewBag.Alumno = alumno;

            return View();
        }



        [HttpPost]
        public IActionResult GuardarEvaluacionDocente(string DocenteMateriaId, List<RespuestaDocente> Respuestas, string Comentario)
        {
            var usuarioId = HttpContext.Session.GetString("UsuarioId");
            var alumno = _context.Alumnos.FirstOrDefault(a => a.Matricula == usuarioId);
            if (alumno == null) return RedirectToAction("Index", "Login");

            var dm = _context.DocenteMaterias.FirstOrDefault(d => d.Id == DocenteMateriaId);
            if (dm == null || Respuestas == null || Respuestas.Any(r => r.Valor < 1 || r.Valor > 5))
                return BadRequest("Respuestas inválidas.");

            var evaluacionId = Guid.NewGuid().ToString();
            var evaluacion = new Evaluacion
            {
                EvaluacionId = evaluacionId,
                Matricula = alumno.Matricula,
                DocenteId = dm.DocenteId,
                MateriaId = dm.MateriaId
            };

            _context.Evaluaciones.Add(evaluacion);

            foreach (var r in Respuestas)
            {
                r.Id = Guid.NewGuid().ToString();
                r.EvaluacionId = evaluacionId;
                _context.RespuestasDocente.Add(r);
            }

            if (!string.IsNullOrEmpty(Comentario))
            {
                _context.Comentarios.Add(new Comentario
                {
                    ComentarioId = Guid.NewGuid().ToString(),
                    Matricula = alumno.Matricula,
                    EvaluacionTipo = "Docente",
                    ReferenciaId = evaluacionId,
                    Contenido = Comentario
                });
            }

            _context.SaveChanges();

            TempData["Mensaje"] = "Evaluación enviada correctamente.";
            return RedirectToAction("EvaluarDocente");
        }

        public IActionResult FormularioDocente(string docenteMateriaId)
        {
            var usuarioId = HttpContext.Session.GetString("UsuarioId");
            var alumno = _context.Alumnos.FirstOrDefault(a => a.Matricula == usuarioId);
            if (alumno == null) return RedirectToAction("Index", "Login");

            var dm = _context.DocenteMaterias.FirstOrDefault(x => x.Id == docenteMateriaId);
            if (dm == null) return NotFound();

            var yaEvaluado = _context.Evaluaciones.Any(e =>
                e.Matricula == alumno.Matricula && e.DocenteId == dm.DocenteId && e.MateriaId == dm.MateriaId);

            if (yaEvaluado)
            {
                TempData["Alerta"] = "Ya evaluaste a este docente.";
                return RedirectToAction("EvaluarDocente");
            }

            var preguntas = _context.PreguntasDocentes.ToList();
            ViewBag.Preguntas = preguntas;
            ViewBag.Docente = _context.Docentes.Find(dm.DocenteId);
            ViewBag.Materia = _context.Materias.Find(dm.MateriaId);
            ViewBag.DocenteMateriaId = docenteMateriaId;

            return View();
        }

        public IActionResult EvaluarDepartamento()
        {
            var matricula = HttpContext.Session.GetString("UsuarioId");
            if (string.IsNullOrEmpty(matricula))
                return RedirectToAction("Index", "Login");

            var alumno = _context.Alumnos.FirstOrDefault(a => a.Matricula == matricula);
            var departamentos = _context.Departamentos.ToList();

            ViewBag.Alumno = alumno;
            ViewBag.Departamentos = departamentos;

            return View();
        }

        public IActionResult FormularioDepartamento(string departamentoId)
        {
            var usuarioId = HttpContext.Session.GetString("UsuarioId");
            var alumno = _context.Alumnos.FirstOrDefault(a => a.Matricula == usuarioId);
            if (alumno == null) return RedirectToAction("Index", "Login");

            var yaEvaluado = _context.EvaluacionesDepartamentos.Any(e =>
                e.Matricula == alumno.Matricula && e.DepartamentoId == departamentoId);

            if (yaEvaluado)
            {
                TempData["Alerta"] = "Ya evaluaste este departamento.";
                return RedirectToAction("EvaluarDepartamento");
            }

            var preguntas = _context.PreguntasDepartamentos
                .Where(p => p.DepartamentoId == departamentoId)
                .ToList();

            ViewBag.Preguntas = preguntas;
            ViewBag.Departamento = _context.Departamentos.Find(departamentoId);
            return View();
        }

        [HttpPost]
        public IActionResult GuardarEvaluacionDepartamento(string DepartamentoId, List<RespuestaDepartamento> Respuestas, string Comentario)
        {
            var usuarioId = HttpContext.Session.GetString("UsuarioId");
            var alumno = _context.Alumnos.FirstOrDefault(a => a.Matricula == usuarioId);
            if (alumno == null) return RedirectToAction("Index", "Login");

            var evaluacionDepId = Guid.NewGuid().ToString();

            var evaluacion = new EvaluacionDepartamento
            {
                EvaluacionDepId = evaluacionDepId,
                Matricula = alumno.Matricula,
                DepartamentoId = DepartamentoId
            };
            _context.EvaluacionesDepartamentos.Add(evaluacion);

            foreach (var r in Respuestas)
            {
                r.Id = Guid.NewGuid().ToString();
                r.EvaluacionDepId = evaluacionDepId;
                _context.RespuestasDepartamento.Add(r);
            }

            if (!string.IsNullOrEmpty(Comentario))
            {
                _context.Comentarios.Add(new Comentario
                {
                    ComentarioId = Guid.NewGuid().ToString(),
                    Matricula = alumno.Matricula,
                    EvaluacionTipo = "Departamento",
                    ReferenciaId = evaluacionDepId,
                    Contenido = Comentario
                });
            }

            _context.SaveChanges();

            TempData["Mensaje"] = "Evaluación del departamento enviada.";
            return RedirectToAction("EvaluarDepartamento");
        }
    }
}
