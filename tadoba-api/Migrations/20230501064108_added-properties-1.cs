using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tadoba_api.Migrations
{
    /// <inheritdoc />
    public partial class addedproperties1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "OrderNo",
                table: "orders",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "TransactionId",
                table: "orders",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_orders_AddressId",
                table: "orders",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_useraddress_AddressId",
                table: "orders",
                column: "AddressId",
                principalTable: "useraddress",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_useraddress_AddressId",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_orders_AddressId",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "OrderNo",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "orders");
        }
    }
}
