using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eva_Sxxi_Prepa_2025.Models
{
    public class Evaluacion
    {
        [Key]
        public required string EvaluacionId { get; set; }

        public required string Matricula { get; set; }

        public required string DocenteId { get; set; }
        public required string MateriaId { get; set; }

        public DateTime FechaEvaluacion { get; set; } = DateTime.Now;

        // 🔽 Propiedad de navegación para respuestas
        public List<RespuestaDocente>? Respuestas { get; set; }
    }
}
