using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace In9Manager.Migrations
{
    /// <inheritdoc />
    public partial class ClienteEndereco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClienteEndereco",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoLogradouro = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Logradouro = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    NumeroLogradouro = table.Column<int>(type: "int", nullable: false),
                    Bairro = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Complemento = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Cep = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    ClienteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClienteEndereco", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClienteEndereco_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClienteEndereco_ClienteId",
                table: "ClienteEndereco",
                column: "ClienteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClienteEndereco");
        }
    }
}
