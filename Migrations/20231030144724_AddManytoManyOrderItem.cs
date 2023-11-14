using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class AddManytoManyOrderItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_item_promotion_product_id",
                table: "order_item");

            migrationBuilder.AddColumn<int>(
                name: "PromotionId1",
                table: "order_item",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_order_item_PromotionId1",
                table: "order_item",
                column: "PromotionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_order_item_promotion_PromotionId1",
                table: "order_item",
                column: "PromotionId1",
                principalTable: "promotion",
                principalColumn: "promotion_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_item_promotion_PromotionId1",
                table: "order_item");

            migrationBuilder.DropIndex(
                name: "IX_order_item_PromotionId1",
                table: "order_item");

            migrationBuilder.DropColumn(
                name: "PromotionId1",
                table: "order_item");

            migrationBuilder.AddForeignKey(
                name: "FK_order_item_promotion_product_id",
                table: "order_item",
                column: "product_id",
                principalTable: "promotion",
                principalColumn: "promotion_id");
        }
    }
}
