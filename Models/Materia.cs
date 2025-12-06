using System.ComponentModel.DataAnnotations;

namespace Eva_Sxxi_Prepa_2025.Models
{
    public class Materia
    {
        [Key]
        public required string MateriaId { get; set; }
        public required string Nombre { get; set; }
    }
}
