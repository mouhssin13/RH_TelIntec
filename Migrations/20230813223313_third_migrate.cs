using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Telintec_RH.Migrations
{
    /// <inheritdoc />
    public partial class third_migrate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departement",
                schema: "TIRH",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nom = table.Column<string>(type: "varchar(100)", nullable: false),
                    chef = table.Column<string>(type: "varchar(250)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departement", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                schema: "TIRH",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fulleName = table.Column<string>(type: "varchar(100)", nullable: false),
                    cin = table.Column<string>(type: "varchar(250)", nullable: false),
                    Tel = table.Column<string>(type: "varchar(100)", nullable: false),
                    statu = table.Column<string>(type: "varchar(250)", nullable: false),
                    poste = table.Column<string>(type: "varchar(100)", nullable: false),
                    DepartementID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Employee_Departement_DepartementID",
                        column: x => x.DepartementID,
                        principalSchema: "TIRH",
                        principalTable: "Departement",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employee_DepartementID",
                schema: "TIRH",
                table: "Employee",
                column: "DepartementID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employee",
                schema: "TIRH");

            migrationBuilder.DropTable(
                name: "Departement",
                schema: "TIRH");
        }
    }
}
