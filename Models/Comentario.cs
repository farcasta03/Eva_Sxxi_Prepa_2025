using System.ComponentModel.DataAnnotations;

namespace Eva_Sxxi_Prepa_2025.Models
{
    public class Comentario
    {
        [Key]
        public required string ComentarioId { get; set; }
        public required string Matricula { get; set; }
        public required string EvaluacionTipo { get; set; } // "Docente" o "Departamento"
        public required string ReferenciaId { get; set; }
        public required string Contenido { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}
