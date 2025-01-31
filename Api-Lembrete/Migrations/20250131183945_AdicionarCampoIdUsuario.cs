using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api_Lembrete.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarCampoIdUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IdUsuario",
                table: "Lembrete",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdUsuario",
                table: "Lembrete");
        }
    }
}
