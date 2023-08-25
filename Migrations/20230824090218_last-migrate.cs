using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Telintec_RH.Migrations
{
    /// <inheritdoc />
    public partial class lastmigrate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Operation",
                schema: "TIRH",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    contrat = table.Column<string>(type: "varchar(250)", nullable: true),
                    montant = table.Column<double>(type: "float", nullable: false),
                    EmployeeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operation", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Operation_Employee_EmployeeID",
                        column: x => x.EmployeeID,
                        principalSchema: "TIRH",
                        principalTable: "Employee",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Operation_EmployeeID",
                schema: "TIRH",
                table: "Operation",
                column: "EmployeeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Operation",
                schema: "TIRH");
        }
    }
}
