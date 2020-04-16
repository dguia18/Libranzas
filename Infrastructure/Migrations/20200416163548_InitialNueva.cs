using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class InitialNueva : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Credito_Empleado_EmpleadoId",
                table: "Credito");

            migrationBuilder.AlterColumn<int>(
                name: "EmpleadoId",
                table: "Credito",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<double>(
                name: "TasaDeInteres",
                table: "Credito",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddForeignKey(
                name: "FK_Credito_Empleado_EmpleadoId",
                table: "Credito",
                column: "EmpleadoId",
                principalTable: "Empleado",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Credito_Empleado_EmpleadoId",
                table: "Credito");

            migrationBuilder.DropColumn(
                name: "TasaDeInteres",
                table: "Credito");

            migrationBuilder.AlterColumn<int>(
                name: "EmpleadoId",
                table: "Credito",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Credito_Empleado_EmpleadoId",
                table: "Credito",
                column: "EmpleadoId",
                principalTable: "Empleado",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
