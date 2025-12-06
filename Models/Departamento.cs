using System.ComponentModel.DataAnnotations;

namespace Eva_Sxxi_Prepa_2025.Models
{
    public class Departamento
    {
        [Key]
        public required string DepartamentoId { get; set; }
        public required string Nombre { get; set; }
    }
}
