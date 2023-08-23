using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Telintec_RH.Migrations
{
    /// <inheritdoc />
    public partial class fourth_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "image",
                schema: "TIRH",
                table: "Employee",
                type: "varchar(250)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(250)");

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
                name: "IX_Holiday_EmployeeID",
                schema: "TIRH",
                table: "Holiday",
                column: "EmployeeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Holiday",
                schema: "TIRH");

            migrationBuilder.AlterColumn<string>(
                name: "image",
                schema: "TIRH",
                table: "Employee",
                type: "varchar(250)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldNullable: true);
        }
    }
}
