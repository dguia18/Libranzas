using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovimientoFinanciero");

            migrationBuilder.DropTable(
                name: "CuentaBancaria");

            migrationBuilder.CreateTable(
                name: "Empleados",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cedula = table.Column<string>(nullable: true),
                    Nombre = table.Column<string>(nullable: true),
                    Salario = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Creditos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero = table.Column<string>(nullable: true),
                    Valor = table.Column<double>(nullable: false),
                    Plazo = table.Column<int>(nullable: false),
                    Saldo = table.Column<double>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    EmpleadoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Creditos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Creditos_Empleados_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Empleados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Abonos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Valor = table.Column<double>(nullable: false),
                    FechaAbonado = table.Column<DateTime>(nullable: false),
                    CreditoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abonos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Abonos_Creditos_CreditoId",
                        column: x => x.CreditoId,
                        principalTable: "Creditos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cuotas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Valor = table.Column<double>(nullable: false),
                    Pagado = table.Column<double>(nullable: false),
                    FechaDePago = table.Column<DateTime>(nullable: false),
                    Estado = table.Column<int>(nullable: false),
                    Saldo = table.Column<double>(nullable: false),
                    CreditoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuotas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cuotas_Creditos_CreditoId",
                        column: x => x.CreditoId,
                        principalTable: "Creditos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Abonos_CreditoId",
                table: "Abonos",
                column: "CreditoId");

            migrationBuilder.CreateIndex(
                name: "IX_Creditos_EmpleadoId",
                table: "Creditos",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Cuotas_CreditoId",
                table: "Cuotas",
                column: "CreditoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Abonos");

            migrationBuilder.DropTable(
                name: "Cuotas");

            migrationBuilder.DropTable(
                name: "Creditos");

            migrationBuilder.DropTable(
                name: "Empleados");

            migrationBuilder.CreateTable(
                name: "CuentaBancaria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Numero = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Saldo = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuentaBancaria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovimientoFinanciero",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CuentaBancariaId = table.Column<int>(type: "int", nullable: true),
                    FechaMovimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValorConsignacion = table.Column<double>(type: "float", nullable: false),
                    ValorRetiro = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimientoFinanciero", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovimientoFinanciero_CuentaBancaria_CuentaBancariaId",
                        column: x => x.CuentaBancariaId,
                        principalTable: "CuentaBancaria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovimientoFinanciero_CuentaBancariaId",
                table: "MovimientoFinanciero",
                column: "CuentaBancariaId");
        }
    }
}
