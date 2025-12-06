using System.ComponentModel.DataAnnotations;

namespace Eva_Sxxi_Prepa_2025.Models
{
    public class Usuario
    {
        [Key]
        public string UsuarioId { get; set; } = string.Empty;

        [Required]
        public string Contraseña { get; set; } = string.Empty;

        [Required]
        public string Rol { get; set; } = "Alumno"; // o "Administrador"
    }
}
