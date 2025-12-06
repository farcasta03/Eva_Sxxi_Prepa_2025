Eva Sxxi Prepa 2025  
Sistema de Evaluación Integral – Preparatoria Siglo XXI

Este proyecto es un "Sistema de Evaluación Integral Académica y Administrativa" utilizado en la "Preparatoria Siglo XXI", desarrollado con "ASP.NET Core MVC", "Entity Framework Core" y "SQL Server".

Su propósito es permitir que los alumnos evalúen a docentes y departamentos, generar reportes en PDF, administrar catálogos institucionales y visualizar resultados de forma clara y eficiente.



Tecnologías Utilizadas

1. ASP.NET Core MVC (.NET 8)
2. Entity Framework Core (Code First + Migrations)
3. SQL Server
4. Razor Pages
5. Bootstrap 5
6. Rotativa (Generación de archivos PDF)
7. C# / LINQ
8. HTML, CSS, JavaScript



Funcionalidades Principales

👩‍🏫 Evaluaciones
- Evaluación de docentes por parte de los alumnos.
- Evaluación de departamentos administrativos.
- Formularios dinámicos basados en preguntas almacenadas en base de datos.
- Sistema de captura validada por matrícula.

📊 Reportes
Reportes por:
  - Docente  
  - Departamento  
  - Periodo escolar  
- Exportación de evaluaciones en PDF.


 Administración
- Gestión de alumnos 
- Gestión de docentes  
- Gestión de materias 
- Gestión de departamentos 
- Gestión de preguntas por área evaluada  

 Autenticación / Seguridad
- Login de Administrador.
- Acceso individual para alumnos mediante matrícula.
- Roles básicos para control de vistas.


Base de Datos

El proyecto utiliza Entity Framework Core  con migraciones.

Puedes reconstruir la base de datos de dos formas:


Opción 1: Usando Migraciones de EF Core
En la consola de Package Manager de Visual Studio:
powershell
"Update-Database"

O con .NET CLI:
"otnet ef database update"


Opción 2: Usando el Script SQL : Dentro del repositorio incluye el archivo el cual contiene la estructura y los datos iniciales:
BD_EvaluacionIntegral.sql




Configuración del Proyecto
1. Para duplicar el archivo:
appsettings.json.example  →  appsettings.json

2. Dentro del nuevo archivo, configura la cadena de conexión:
  "ConnectionStrings": {
  "DefaultConnection": "Server=TU_SERVIDOR;Database=EvaluacionSXXI;User=USUARIO;Password=CONTRASEÑA;"
}

GUARDAR LOS CAMBIOS Y EJECUTAR EL PROYECTO



  PARA LA EJECUCIÓN DEL PROYECTO:
  1. Abrir el archivo de solución:
     Eva_Sxxi_Prepa_2025.sln
  2. Se puede ejecutar con:
     -  IIS Express, o Kestrel (Project)
  3. Abir en el navegador lo que indica la URL de vs



ESTRUCTURA GENERAL DE TODO EL PORYECTO:
Eva_Sxxi_Prepa_2025/
│── Controllers/
│── Models/
│── Migrations/
│── Views/
│── wwwroot/
│── appsettings.json.example
│── BD_EvaluacionIntegral.sql
│── Eva_Sxxi_Prepa_2025.csproj
│── Program.cs
└── README.md


Este sistema fue desarrollado como parte de un proyecto académico para digitalizar y optimizar el proceso de evaluación institucional, proporcionando:

- Formularios modernos
- Validación de usuarios
- Reportes PDF
- Gestión completa de datos
- Navegación amigable para alumnos y administradores



Autor
**José Manuel Castañeda Garfias**
**Desarrollador – Preparatoria Siglo XXI**
**GitHub: farcasta03**


**Licencia*
**Proyecto de uso académico y de portafolio.**
**No se autoriza su distribución comercial.*
