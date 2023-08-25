using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Telintec_RH.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
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
                    image = table.Column<string>(type: "varchar(250)", nullable: true),
                    Tel = table.Column<string>(type: "varchar(100)", nullable: false),
                    statu = table.Column<string>(type: "varchar(250)", nullable: false),
                    salary = table.Column<double>(type: "float", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "Absence",
                schema: "TIRH",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date_Dep = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Date_Fin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    reason = table.Column<string>(type: "varchar(250)", nullable: true),
                    EmployeeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Absence", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Absence_Employee_EmployeeID",
                        column: x => x.EmployeeID,
                        principalSchema: "TIRH",
                        principalTable: "Employee",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Holiday",
                schema: "TIRH",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date_Dep = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Date_Fin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holiday", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Holiday_Employee_EmployeeID",
                        column: x => x.EmployeeID,
                        principalSchema: "TIRH",
                        principalTable: "Employee",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Absence_EmployeeID",
                schema: "TIRH",
                table: "Absence",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_DepartementID",
                schema: "TIRH",
                table: "Employee",
                column: "DepartementID");

            migrationBuilder.CreateIndex(
                name: "IX_Holiday_EmployeeID",
                schema: "TIRH",
                table: "Holiday",
                column: "EmployeeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Absence",
                schema: "TIRH");

            migrationBuilder.DropTable(
                name: "Account",
                schema: "TIRH");

            migrationBuilder.DropTable(
                name: "Holiday",
                schema: "TIRH");

            migrationBuilder.DropTable(
                name: "Employee",
                schema: "TIRH");

            migrationBuilder.DropTable(
                name: "Departement",
                schema: "TIRH");
        }
    }
}
