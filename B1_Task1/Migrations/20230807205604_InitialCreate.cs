using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace B1_Task1.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TextFromFile",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    LatinString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RussianString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IntegerNum = table.Column<int>(type: "int", nullable: false),
                    FloatNum = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextFromFile", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TextFromFile");
        }
    }
}
