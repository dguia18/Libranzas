using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Abonos_Creditos_CreditoId",
                table: "Abonos");

            migrationBuilder.DropForeignKey(
                name: "FK_Creditos_Empleados_EmpleadoId",
                table: "Creditos");

            migrationBuilder.DropForeignKey(
                name: "FK_Cuotas_Creditos_CreditoId",
                table: "Cuotas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Empleados",
                table: "Empleados");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cuotas",
                table: "Cuotas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Creditos",
                table: "Creditos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Abonos",
                table: "Abonos");

            migrationBuilder.RenameTable(
                name: "Empleados",
                newName: "Empleado");

            migrationBuilder.RenameTable(
                name: "Cuotas",
                newName: "Cuota");

            migrationBuilder.RenameTable(
                name: "Creditos",
                newName: "Credito");

            migrationBuilder.RenameTable(
                name: "Abonos",
                newName: "Abono");

            migrationBuilder.RenameIndex(
                name: "IX_Cuotas_CreditoId",
                table: "Cuota",
                newName: "IX_Cuota_CreditoId");

            migrationBuilder.RenameIndex(
                name: "IX_Creditos_EmpleadoId",
                table: "Credito",
                newName: "IX_Credito_EmpleadoId");

            migrationBuilder.RenameIndex(
                name: "IX_Abonos_CreditoId",
                table: "Abono",
                newName: "IX_Abono_CreditoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Empleado",
                table: "Empleado",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cuota",
                table: "Cuota",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Credito",
                table: "Credito",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Abono",
                table: "Abono",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Abono_Credito_CreditoId",
                table: "Abono",
                column: "CreditoId",
                principalTable: "Credito",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Credito_Empleado_EmpleadoId",
                table: "Credito",
                column: "EmpleadoId",
                principalTable: "Empleado",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cuota_Credito_CreditoId",
                table: "Cuota",
                column: "CreditoId",
                principalTable: "Credito",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Abono_Credito_CreditoId",
                table: "Abono");

            migrationBuilder.DropForeignKey(
                name: "FK_Credito_Empleado_EmpleadoId",
                table: "Credito");

            migrationBuilder.DropForeignKey(
                name: "FK_Cuota_Credito_CreditoId",
                table: "Cuota");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Empleado",
                table: "Empleado");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cuota",
                table: "Cuota");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Credito",
                table: "Credito");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Abono",
                table: "Abono");

            migrationBuilder.RenameTable(
                name: "Empleado",
                newName: "Empleados");

            migrationBuilder.RenameTable(
                name: "Cuota",
                newName: "Cuotas");

            migrationBuilder.RenameTable(
                name: "Credito",
                newName: "Creditos");

            migrationBuilder.RenameTable(
                name: "Abono",
                newName: "Abonos");

            migrationBuilder.RenameIndex(
                name: "IX_Cuota_CreditoId",
                table: "Cuotas",
                newName: "IX_Cuotas_CreditoId");

            migrationBuilder.RenameIndex(
                name: "IX_Credito_EmpleadoId",
                table: "Creditos",
                newName: "IX_Creditos_EmpleadoId");

            migrationBuilder.RenameIndex(
                name: "IX_Abono_CreditoId",
                table: "Abonos",
                newName: "IX_Abonos_CreditoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Empleados",
                table: "Empleados",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cuotas",
                table: "Cuotas",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Creditos",
                table: "Creditos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Abonos",
                table: "Abonos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Abonos_Creditos_CreditoId",
                table: "Abonos",
                column: "CreditoId",
                principalTable: "Creditos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Creditos_Empleados_EmpleadoId",
                table: "Creditos",
                column: "EmpleadoId",
                principalTable: "Empleados",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cuotas_Creditos_CreditoId",
                table: "Cuotas",
                column: "CreditoId",
                principalTable: "Creditos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
