namespace Eva_Sxxi_Prepa_2025.Models.ViewModels
{
    public class ReporteDocenteViewModel
    {
        public string Docente { get; set; }
        public string Grupo { get; set; }
        public string Semestre { get; set; }
        public string Promedio { get; set; }
        public int Evaluaciones { get; set; }
        public List<string> Comentarios { get; set; }
    }
}
