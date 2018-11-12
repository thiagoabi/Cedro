using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Restaurante.Infrastructure.Context.Migrations
{
    public partial class RestauranteMigration_Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Restaurantes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CodigoInterno = table.Column<string>(nullable: true),
                    DataInclusao = table.Column<DateTime>(nullable: false),
                    UsuarioInclusao = table.Column<string>(nullable: true),
                    DataUltimaAlteracao = table.Column<DateTime>(nullable: true),
                    UsuarioUltimaAlteracao = table.Column<string>(nullable: true),
                    Nome = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurantes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pratos",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CodigoInterno = table.Column<string>(nullable: true),
                    DataInclusao = table.Column<DateTime>(nullable: false),
                    UsuarioInclusao = table.Column<string>(nullable: true),
                    DataUltimaAlteracao = table.Column<DateTime>(nullable: true),
                    UsuarioUltimaAlteracao = table.Column<string>(nullable: true),
                    EstabelecimentoId = table.Column<long>(nullable: false),
                    Nome = table.Column<string>(maxLength: 100, nullable: true),
                    Valor = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pratos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pratos_Restaurantes_EstabelecimentoId",
                        column: x => x.EstabelecimentoId,
                        principalTable: "Restaurantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pratos_EstabelecimentoId",
                table: "Pratos",
                column: "EstabelecimentoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pratos");

            migrationBuilder.DropTable(
                name: "Restaurantes");
        }
    }
}
