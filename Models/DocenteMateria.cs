using System.ComponentModel.DataAnnotations;

namespace Eva_Sxxi_Prepa_2025.Models
{
    public class DocenteMateria
    {
        [Key]
        public required string Id { get; set; }
        public required string DocenteId { get; set; }
        public required string MateriaId { get; set; }
        // En tu modelo
        public required string Semestre { get; set; } // o int ?
        public required string Grupo { get; set; }    // o int ?


    }
}
