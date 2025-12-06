using Microsoft.EntityFrameworkCore;
using Eva_Sxxi_Prepa_2025.Models;

namespace Eva_Sxxi_Prepa_2025.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        // Agrega tus entidades
        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Docente> Docentes { get; set; }
        public DbSet<Materia> Materias { get; set; }
        public DbSet<DocenteMateria> DocenteMaterias { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<PreguntaDocente> PreguntasDocentes { get; set; }
        public DbSet<PreguntaDepartamento> PreguntasDepartamentos { get; set; }
        public DbSet<Evaluacion> Evaluaciones { get; set; }
        public DbSet<RespuestaDocente> RespuestasDocente { get; set; }
        public DbSet<RespuestaDepartamento> RespuestasDepartamento { get; set; }
        public DbSet<EvaluacionDepartamento> EvaluacionesDepartamentos { get; set; }

        public DbSet<Comentario> Comentarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RespuestaDocente>()
                .HasOne(r => r.Evaluacion)
                .WithMany()
                .HasForeignKey(r => r.EvaluacionId);

            base.OnModelCreating(modelBuilder);
        }
    }

    }
