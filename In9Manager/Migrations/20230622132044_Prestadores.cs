using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace In9Manager.Migrations
{
    /// <inheritdoc />
    public partial class Prestadores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClienteEndereco_Cliente_ClienteId",
                table: "ClienteEndereco");

            migrationBuilder.DropForeignKey(
                name: "FK_Veiculos_Cliente_ClienteID",
                table: "Veiculos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClienteEndereco",
                table: "ClienteEndereco");

            migrationBuilder.DropUniqueConstraint(
                name: "CPF",
                table: "Cliente");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cliente",
                table: "Cliente");

            migrationBuilder.RenameTable(
                name: "ClienteEndereco",
                newName: "ClienteEnderecos");

            migrationBuilder.RenameTable(
                name: "Cliente",
                newName: "Clientes");

            migrationBuilder.RenameIndex(
                name: "IX_ClienteEndereco_ClienteId",
                table: "ClienteEnderecos",
                newName: "IX_ClienteEnderecos_ClienteId");

            migrationBuilder.RenameIndex(
                name: "IX_Cliente_CPF",
                table: "Clientes",
                newName: "IX_Clientes_CPF");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClienteEnderecos",
                table: "ClienteEnderecos",
                column: "Id");

            migrationBuilder.AddUniqueConstraint(
                name: "CPF_Cliente",
                table: "Clientes",
                column: "CPF");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clientes",
                table: "Clientes",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Prestadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    TipoPrestador = table.Column<int>(type: "int", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prestadores", x => x.Id);
                    table.UniqueConstraint("CPF_Prestador", x => x.CPF);
                });

            migrationBuilder.CreateTable(
                name: "PrestadorEnderecos",
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
                    Cep = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    PrestadorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrestadorEnderecos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrestadorEnderecos_Prestadores_PrestadorId",
                        column: x => x.PrestadorId,
                        principalTable: "Prestadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrestadorEnderecos_PrestadorId",
                table: "PrestadorEnderecos",
                column: "PrestadorId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClienteEnderecos_Clientes_ClienteId",
                table: "ClienteEnderecos",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Veiculos_Clientes_ClienteID",
                table: "Veiculos",
                column: "ClienteID",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClienteEnderecos_Clientes_ClienteId",
                table: "ClienteEnderecos");

            migrationBuilder.DropForeignKey(
                name: "FK_Veiculos_Clientes_ClienteID",
                table: "Veiculos");

            migrationBuilder.DropTable(
                name: "PrestadorEnderecos");

            migrationBuilder.DropTable(
                name: "Prestadores");

            migrationBuilder.DropUniqueConstraint(
                name: "CPF_Cliente",
                table: "Clientes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clientes",
                table: "Clientes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClienteEnderecos",
                table: "ClienteEnderecos");

            migrationBuilder.RenameTable(
                name: "Clientes",
                newName: "Cliente");

            migrationBuilder.RenameTable(
                name: "ClienteEnderecos",
                newName: "ClienteEndereco");

            migrationBuilder.RenameIndex(
                name: "IX_Clientes_CPF",
                table: "Cliente",
                newName: "IX_Cliente_CPF");

            migrationBuilder.RenameIndex(
                name: "IX_ClienteEnderecos_ClienteId",
                table: "ClienteEndereco",
                newName: "IX_ClienteEndereco_ClienteId");

            migrationBuilder.AddUniqueConstraint(
                name: "CPF",
                table: "Cliente",
                column: "CPF");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cliente",
                table: "Cliente",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClienteEndereco",
                table: "ClienteEndereco",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClienteEndereco_Cliente_ClienteId",
                table: "ClienteEndereco",
                column: "ClienteId",
                principalTable: "Cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Veiculos_Cliente_ClienteID",
                table: "Veiculos",
                column: "ClienteID",
                principalTable: "Cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
