using Eva_Sxxi_Prepa_2025.Data;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore; // ✅ Rotativa

var builder = WebApplication.CreateBuilder(args);

// 🧩 1. Configurar conexión a la base de datos
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ 2. Habilitar sesiones (para login)
builder.Services.AddSession();

// ✅ 3. Habilitar controladores con vistas
builder.Services.AddControllersWithViews();

// (Opcional) Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ 4. Build de la aplicación
var app = builder.Build();

// ✅ 5. Configurar middlewares
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

// ✅ 6. Configurar uso de Rotativa
RotativaConfiguration.Setup(app.Environment.WebRootPath, "Rotativa"); // ⬅ importante

// ✅ 7. Rutas por defecto
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

// ✅ 8. Ejecutar
app.Run();
