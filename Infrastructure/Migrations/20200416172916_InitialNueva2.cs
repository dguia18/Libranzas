using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class InitialNueva2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CuotaId",
                table: "Abono",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Abono_CuotaId",
                table: "Abono",
                column: "CuotaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Abono_Cuota_CuotaId",
                table: "Abono",
                column: "CuotaId",
                principalTable: "Cuota",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Abono_Cuota_CuotaId",
                table: "Abono");

            migrationBuilder.DropIndex(
                name: "IX_Abono_CuotaId",
                table: "Abono");

            migrationBuilder.DropColumn(
                name: "CuotaId",
                table: "Abono");
        }
    }
}
