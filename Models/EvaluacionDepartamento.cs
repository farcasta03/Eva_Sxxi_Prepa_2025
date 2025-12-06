using System.ComponentModel.DataAnnotations;

namespace Eva_Sxxi_Prepa_2025.Models
{
    public class EvaluacionDepartamento
    {
        [Key]
        public required string EvaluacionDepId { get; set; }

        public required string Matricula { get; set; }
        public required string DepartamentoId { get; set; }

        // Relación
        public Departamento? Departamento { get; set; }
    }
}
