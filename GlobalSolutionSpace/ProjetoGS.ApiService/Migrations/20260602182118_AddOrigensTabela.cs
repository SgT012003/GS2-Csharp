using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoGS.ApiService.Migrations
{
    /// <inheritdoc />
    public partial class AddOrigensTabela : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrigemEspacial",
                table: "Tecnologias");

            migrationBuilder.AddColumn<int>(
                name: "OrigemId",
                table: "Tecnologias",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Origens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descricao = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Origens", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Tecnologias_OrigemId",
                table: "Tecnologias",
                column: "OrigemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tecnologias_Origens_OrigemId",
                table: "Tecnologias",
                column: "OrigemId",
                principalTable: "Origens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tecnologias_Origens_OrigemId",
                table: "Tecnologias");

            migrationBuilder.DropTable(
                name: "Origens");

            migrationBuilder.DropIndex(
                name: "IX_Tecnologias_OrigemId",
                table: "Tecnologias");

            migrationBuilder.DropColumn(
                name: "OrigemId",
                table: "Tecnologias");

            migrationBuilder.AddColumn<string>(
                name: "OrigemEspacial",
                table: "Tecnologias",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
