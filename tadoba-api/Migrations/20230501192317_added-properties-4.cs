using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tadoba_api.Migrations
{
    /// <inheritdoc />
    public partial class addedproperties4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "dropdownmaster",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "dropdownmaster");
        }
    }
}
