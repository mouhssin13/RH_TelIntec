using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Telintec_RH.Migrations
{
    /// <inheritdoc />
    public partial class seconde_migrate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "TIRH");

            migrationBuilder.CreateTable(
                name: "Account",
                schema: "TIRH",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstName = table.Column<string>(type: "varchar(100)", nullable: false),
                    lastName = table.Column<string>(type: "varchar(100)", nullable: false),
                    userName = table.Column<string>(type: "varchar(250)", nullable: false),
                    Tel = table.Column<string>(type: "varchar(100)", nullable: false),
                    password = table.Column<string>(type: "varchar(250)", nullable: false),
                    role = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Account",
                schema: "TIRH");
        }
    }
}
