using System.ComponentModel.DataAnnotations;

namespace Eva_Sxxi_Prepa_2025.Models
{
    public class PreguntaDocente
    {
        [Key]
        public required string PreguntaId { get; set; }
        public required string Texto { get; set; }
    }
}
