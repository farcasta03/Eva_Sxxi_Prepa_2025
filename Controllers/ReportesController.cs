using Microsoft.AspNetCore.Mvc;
using Eva_Sxxi_Prepa_2025.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Eva_Sxxi_Prepa_2025.Models.ViewModels;
using Eva_Sxxi_Prepa_2025.Models;
using Rotativa.AspNetCore; // Necesario para exportar PDF

namespace Eva_Sxxi_Prepa_2025.Controllers
{
    public class ReportesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult EvaluacionDocentes(string docenteId, string semestre, string grupo)
        {
            var respuestas = _context.RespuestasDocente
                .Include(r => r.Evaluacion)
                .ToList();

            var resultados = respuestas
                .Where(r =>
                    (string.IsNullOrEmpty(docenteId) || r.Evaluacion.DocenteId == docenteId) &&
                    (string.IsNullOrEmpty(semestre) || _context.Alumnos.FirstOrDefault(a => a.Matricula == r.Evaluacion.Matricula)?.Semestre == semestre) &&
                    (string.IsNullOrEmpty(grupo) || _context.Alumnos.FirstOrDefault(a => a.Matricula == r.Evaluacion.Matricula)?.Grupo == grupo))
                .GroupBy(r => new
                {
                    DocenteId = r.Evaluacion.DocenteId,
                    Grupo = _context.Alumnos.FirstOrDefault(a => a.Matricula == r.Evaluacion.Matricula)?.Grupo ?? "N/A",
                    Semestre = _context.Alumnos.FirstOrDefault(a => a.Matricula == r.Evaluacion.Matricula)?.Semestre ?? "N/A"
                })
                .Select(g => new ReporteDocenteViewModel
                {
                    Docente = _context.Docentes.FirstOrDefault(d => d.DocenteId == g.Key.DocenteId)?.NombreCompleto ?? "Sin nombre",
                    Grupo = g.Key.Grupo,
                    Semestre = g.Key.Semestre,
                    Promedio = g.Average(x => x.Valor).ToString("0.0"),
                    Evaluaciones = g.Count(),
                    Comentarios = _context.Comentarios
                        .Where(c => c.EvaluacionTipo == "Docente" && g.Select(x => x.EvaluacionId).Contains(c.ReferenciaId))
                        .Select(c => c.Contenido)
                        .ToList()
                })
                .ToList();

            ViewBag.Docentes = _context.Docentes.Select(d => new { d.DocenteId, d.NombreCompleto }).ToList();
            ViewBag.Grupos = _context.Alumnos.Select(a => a.Grupo).Distinct().ToList();
            ViewBag.Semestres = _context.Alumnos.Select(a => a.Semestre).Distinct().ToList();

            ViewBag.DocenteSeleccionado = docenteId;
            ViewBag.GrupoSeleccionado = grupo;
            ViewBag.SemestreSeleccionado = semestre;

            return View("EvaluacionDocentes", resultados);
        }

        public IActionResult ExportarDocentesPDF(string docenteId, string semestre, string grupo)
        {
            var respuestas = _context.RespuestasDocente
                .Include(r => r.Evaluacion)
                .ToList();

            var resultados = respuestas
                .Where(r =>
                    (string.IsNullOrEmpty(docenteId) || r.Evaluacion.DocenteId == docenteId) &&
                    (string.IsNullOrEmpty(semestre) || _context.Alumnos.FirstOrDefault(a => a.Matricula == r.Evaluacion.Matricula)?.Semestre == semestre) &&
                    (string.IsNullOrEmpty(grupo) || _context.Alumnos.FirstOrDefault(a => a.Matricula == r.Evaluacion.Matricula)?.Grupo == grupo))
                .GroupBy(r => new
                {
                    DocenteId = r.Evaluacion.DocenteId,
                    Grupo = _context.Alumnos.FirstOrDefault(a => a.Matricula == r.Evaluacion.Matricula)?.Grupo ?? "N/A",
                    Semestre = _context.Alumnos.FirstOrDefault(a => a.Matricula == r.Evaluacion.Matricula)?.Semestre ?? "N/A"
                })
                .Select(g => new ReporteDocenteViewModel
                {
                    Docente = _context.Docentes.FirstOrDefault(d => d.DocenteId == g.Key.DocenteId)?.NombreCompleto ?? "Sin nombre",
                    Grupo = g.Key.Grupo,
                    Semestre = g.Key.Semestre,
                    Promedio = g.Average(x => x.Valor).ToString("0.0"),
                    Evaluaciones = g.Count(),
                    Comentarios = _context.Comentarios
                        .Where(c => c.EvaluacionTipo == "Docente" && g.Select(x => x.EvaluacionId).Contains(c.ReferenciaId))
                        .Select(c => c.Contenido)
                        .ToList()
                })
                .ToList();

            return new ViewAsPdf("EvaluacionDocentesPDF", resultados)
            {
                FileName = "ReporteDocente.pdf"
            };
        }

        public IActionResult EvaluacionDepartamentos(string departamentoId)
        {
            var respuestas = _context.RespuestasDepartamento
                .Include(r => r.EvaluacionDepartamento)
                    .ThenInclude(e => e.Departamento)
                .ToList();

            var resultados = respuestas
                .Where(r => string.IsNullOrEmpty(departamentoId) || r.EvaluacionDepartamento.DepartamentoId == departamentoId)
                .GroupBy(r => new
                {
                    DepartamentoId = r.EvaluacionDepartamento.DepartamentoId,
                    Departamento = r.EvaluacionDepartamento.Departamento?.Nombre ?? "Desconocido",
                    Grupo = _context.Alumnos.FirstOrDefault(a => a.Matricula == r.EvaluacionDepartamento.Matricula)?.Grupo ?? "N/A"
                })
                .Select(g => new ReporteDepartamentoViewModel
                {
                    Departamento = g.Key.Departamento,
                    Grupo = g.Key.Grupo,
                    Promedio = g.Average(x => x.Valor).ToString("0.0"),
                    Evaluaciones = g.Count(),
                    Comentarios = _context.Comentarios
                        .Where(c => c.EvaluacionTipo == "Departamento" &&
                                    g.Select(x => x.EvaluacionDepartamento.EvaluacionDepId).Contains(c.ReferenciaId))
                        .Select(c => c.Contenido)
                        .ToList()
                })
                .ToList();

            ViewBag.Departamentos = _context.Departamentos
                .Select(d => new { d.DepartamentoId, d.Nombre })
                .ToList();

            ViewBag.DepartamentoSeleccionado = departamentoId;

            return View("EvaluacionDepartamentos", resultados);
        }

        public IActionResult ExportarDepartamentoPDF(string departamentoId)
        {
            var respuestas = _context.RespuestasDepartamento
                .Include(r => r.EvaluacionDepartamento)
                    .ThenInclude(e => e.Departamento)
                .Where(r => r.EvaluacionDepartamento.DepartamentoId == departamentoId)
                .ToList();

            var grupoed = respuestas
                .GroupBy(r => new
                {
                    DepartamentoId = r.EvaluacionDepartamento.DepartamentoId,
                    Departamento = r.EvaluacionDepartamento.Departamento?.Nombre ?? "Desconocido",
                    Grupo = _context.Alumnos.FirstOrDefault(a => a.Matricula == r.EvaluacionDepartamento.Matricula)?.Grupo ?? "N/A"
                })
                .Select(g => new ReporteDepartamentoViewModel
                {
                    Departamento = g.Key.Departamento,
                    Grupo = g.Key.Grupo,
                    Promedio = g.Average(x => x.Valor).ToString("0.0"),
                    Evaluaciones = g.Count(),
                    Comentarios = _context.Comentarios
                        .Where(c => c.EvaluacionTipo == "Departamento" &&
                                    g.Select(x => x.EvaluacionDepartamento.EvaluacionDepId).Contains(c.ReferenciaId))
                        .Select(c => c.Contenido)
                        .ToList()
                })
                .ToList();

            return new ViewAsPdf("EvaluacionDepartamentosPDF", grupoed)
            {
                FileName = "ReporteDepartamento.pdf"
            };
        }

        public IActionResult RegresarAlMenu()
        {
            return RedirectToAction("Menu", "Evaluacion");
        }
    }
}
