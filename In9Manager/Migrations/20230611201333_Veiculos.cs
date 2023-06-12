using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace In9Manager.Migrations
{
    /// <inheritdoc />
    public partial class Veiculos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Veiculos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Modelo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Ano = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    Categoria = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Placa = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false),
                    ClienteID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veiculos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Veiculos_Cliente_ClienteID",
                        column: x => x.ClienteID,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Veiculos_ClienteID",
                table: "Veiculos",
                column: "ClienteID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Veiculos");
        }
    }
}
