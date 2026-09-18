using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimuladoOficina.Api.Migrations
{
    /// <inheritdoc />
    public partial class Katchau : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agendamentos_Clientes_ClienteId",
                table: "Agendamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_Agendamentos_Mecanicos_MecanicoId",
                table: "Agendamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_Agendamentos_Veiculos_VeiculoId",
                table: "Agendamentos");

            migrationBuilder.AddForeignKey(
                name: "FK_Agendamentos_Clientes_ClienteId",
                table: "Agendamentos",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Agendamentos_Mecanicos_MecanicoId",
                table: "Agendamentos",
                column: "MecanicoId",
                principalTable: "Mecanicos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Agendamentos_Veiculos_VeiculoId",
                table: "Agendamentos",
                column: "VeiculoId",
                principalTable: "Veiculos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agendamentos_Clientes_ClienteId",
                table: "Agendamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_Agendamentos_Mecanicos_MecanicoId",
                table: "Agendamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_Agendamentos_Veiculos_VeiculoId",
                table: "Agendamentos");

            migrationBuilder.AddForeignKey(
                name: "FK_Agendamentos_Clientes_ClienteId",
                table: "Agendamentos",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Agendamentos_Mecanicos_MecanicoId",
                table: "Agendamentos",
                column: "MecanicoId",
                principalTable: "Mecanicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Agendamentos_Veiculos_VeiculoId",
                table: "Agendamentos",
                column: "VeiculoId",
                principalTable: "Veiculos",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
