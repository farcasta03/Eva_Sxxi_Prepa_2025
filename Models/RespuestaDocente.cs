using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eva_Sxxi_Prepa_2025.Models
{
    public class RespuestaDocente
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string EvaluacionId { get; set; }

        [ForeignKey("EvaluacionId")]
        public Evaluacion Evaluacion { get; set; }

        public int PreguntaId { get; set; }

        public int Valor { get; set; }
    }
}
