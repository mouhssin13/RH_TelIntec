using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Telintec_RH.Migrations
{
    /// <inheritdoc />
    public partial class _5Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_Absence_EmployeeID",
                schema: "TIRH",
                table: "Absence",
                column: "EmployeeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Absence",
                schema: "TIRH");
        }
    }
}
