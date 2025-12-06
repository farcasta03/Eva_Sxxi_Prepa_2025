using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eva_Sxxi_Prepa_2025.Migrations
{
    /// <inheritdoc />
    public partial class FixEvaluacionIdRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaEvaluacion",
                table: "EvaluacionesDepartamentos");

            migrationBuilder.AlterColumn<int>(
                name: "PreguntaId",
                table: "RespuestasDocente",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "EvaluacionId",
                table: "RespuestasDocente",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "EvaluacionId1",
                table: "RespuestasDocente",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EvaluacionDepId",
                table: "RespuestasDepartamento",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "DepartamentoId",
                table: "EvaluacionesDepartamentos",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Semestre",
                table: "DocenteMaterias",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Semestre",
                table: "Alumnos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_RespuestasDocente_EvaluacionId",
                table: "RespuestasDocente",
                column: "EvaluacionId");

            migrationBuilder.CreateIndex(
                name: "IX_RespuestasDocente_EvaluacionId1",
                table: "RespuestasDocente",
                column: "EvaluacionId1");

            migrationBuilder.CreateIndex(
                name: "IX_RespuestasDepartamento_EvaluacionDepId",
                table: "RespuestasDepartamento",
                column: "EvaluacionDepId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluacionesDepartamentos_DepartamentoId",
                table: "EvaluacionesDepartamentos",
                column: "DepartamentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_EvaluacionesDepartamentos_Departamentos_DepartamentoId",
                table: "EvaluacionesDepartamentos",
                column: "DepartamentoId",
                principalTable: "Departamentos",
                principalColumn: "DepartamentoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RespuestasDepartamento_EvaluacionesDepartamentos_EvaluacionDepId",
                table: "RespuestasDepartamento",
                column: "EvaluacionDepId",
                principalTable: "EvaluacionesDepartamentos",
                principalColumn: "EvaluacionDepId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RespuestasDocente_Evaluaciones_EvaluacionId",
                table: "RespuestasDocente",
                column: "EvaluacionId",
                principalTable: "Evaluaciones",
                principalColumn: "EvaluacionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RespuestasDocente_Evaluaciones_EvaluacionId1",
                table: "RespuestasDocente",
                column: "EvaluacionId1",
                principalTable: "Evaluaciones",
                principalColumn: "EvaluacionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EvaluacionesDepartamentos_Departamentos_DepartamentoId",
                table: "EvaluacionesDepartamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_RespuestasDepartamento_EvaluacionesDepartamentos_EvaluacionDepId",
                table: "RespuestasDepartamento");

            migrationBuilder.DropForeignKey(
                name: "FK_RespuestasDocente_Evaluaciones_EvaluacionId",
                table: "RespuestasDocente");

            migrationBuilder.DropForeignKey(
                name: "FK_RespuestasDocente_Evaluaciones_EvaluacionId1",
                table: "RespuestasDocente");

            migrationBuilder.DropIndex(
                name: "IX_RespuestasDocente_EvaluacionId",
                table: "RespuestasDocente");

            migrationBuilder.DropIndex(
                name: "IX_RespuestasDocente_EvaluacionId1",
                table: "RespuestasDocente");

            migrationBuilder.DropIndex(
                name: "IX_RespuestasDepartamento_EvaluacionDepId",
                table: "RespuestasDepartamento");

            migrationBuilder.DropIndex(
                name: "IX_EvaluacionesDepartamentos_DepartamentoId",
                table: "EvaluacionesDepartamentos");

            migrationBuilder.DropColumn(
                name: "EvaluacionId1",
                table: "RespuestasDocente");

            migrationBuilder.AlterColumn<string>(
                name: "PreguntaId",
                table: "RespuestasDocente",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "EvaluacionId",
                table: "RespuestasDocente",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "EvaluacionDepId",
                table: "RespuestasDepartamento",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "DepartamentoId",
                table: "EvaluacionesDepartamentos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaEvaluacion",
                table: "EvaluacionesDepartamentos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "Semestre",
                table: "DocenteMaterias",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Semestre",
                table: "Alumnos",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
