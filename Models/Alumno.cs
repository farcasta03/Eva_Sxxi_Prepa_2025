using System.ComponentModel.DataAnnotations;

namespace Eva_Sxxi_Prepa_2025.Models
{
    public class Alumno
    {
        [Key]
        
        public required string Matricula { get; set; }
        
        public required string Apellido1 { get; set; }
       
        public required string Apellido2 { get; set; }
      
        public required string Nombre { get; set; }
        
        public required string Semestre { get; set; }
       
        public required string Grupo { get; set; }
    }
}
