using System.ComponentModel.DataAnnotations;

namespace Eva_Sxxi_Prepa_2025.Models
{
    public class Docente
    {
        [Key]
        public required string DocenteId { get; set; }
        public required string NombreCompleto { get; set; }
    }
}
