using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProdutoApp.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PRODUTO",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NOME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DESCRICAO = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    DATAHORA = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PRECO = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    QUANTIDADE = table.Column<int>(type: "int", nullable: false),
                    ESTADO = table.Column<int>(type: "int", nullable: false),
                    CADASTRADOEM = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ULTIMAATUALIZACAOEM = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExcluidoEm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ATIVO = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUTO", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PRODUTO");
        }
    }
}
