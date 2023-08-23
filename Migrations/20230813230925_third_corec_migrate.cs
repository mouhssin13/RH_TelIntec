using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Telintec_RH.Migrations
{
    /// <inheritdoc />
    public partial class third_corec_migrate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "image",
                schema: "TIRH",
                table: "Employee",
                type: "varchar(250)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "salary",
                schema: "TIRH",
                table: "Employee",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "image",
                schema: "TIRH",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "salary",
                schema: "TIRH",
                table: "Employee");
        }
    }
}
