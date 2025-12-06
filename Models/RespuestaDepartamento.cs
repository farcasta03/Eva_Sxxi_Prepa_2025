using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eva_Sxxi_Prepa_2025.Models
{
    public class RespuestaDepartamento
    {
        [Key]
        public required string Id { get; set; }

        public required string EvaluacionDepId { get; set; }

        public required string PreguntaId { get; set; }
        public int Valor { get; set; }

        // Relación
        [ForeignKey("EvaluacionDepId")]
        public EvaluacionDepartamento? EvaluacionDepartamento { get; set; }
    }
}
