using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace In9Manager.Migrations
{
    /// <inheritdoc />
    public partial class AtributosUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "Login",
                table: "Usuarios",
                column: "Login");

            migrationBuilder.AddUniqueConstraint(
                name: "CPF",
                table: "Cliente",
                column: "CPF");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "Login",
                table: "Usuarios");

            migrationBuilder.DropUniqueConstraint(
                name: "CPF",
                table: "Cliente");
        }
    }
}
