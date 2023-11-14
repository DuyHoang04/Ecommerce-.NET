using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class up : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_item_promotion_PromotionId",
                table: "order_item");

            migrationBuilder.DropIndex(
                name: "IX_order_item_PromotionId",
                table: "order_item");

            migrationBuilder.DropColumn(
                name: "PromotionId",
                table: "order_item");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PromotionId",
                table: "order_item",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_order_item_PromotionId",
                table: "order_item",
                column: "PromotionId");

            migrationBuilder.AddForeignKey(
                name: "FK_order_item_promotion_PromotionId",
                table: "order_item",
                column: "PromotionId",
                principalTable: "promotion",
                principalColumn: "promotion_id");
        }
    }
}
