using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class fixs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_item_AspNetUsers_user_id",
                table: "order_item");

            migrationBuilder.DropForeignKey(
                name: "FK_order_item_products_product_id",
                table: "order_item");

            migrationBuilder.DropForeignKey(
                name: "FK_order_item_promotion_PromotionId1",
                table: "order_item");

            migrationBuilder.DropIndex(
                name: "IX_order_item_PromotionId1",
                table: "order_item");

            migrationBuilder.DropColumn(
                name: "PromotionId1",
                table: "order_item");

            migrationBuilder.AlterColumn<string>(
                name: "user_id",
                table: "order_item",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_order_item_AspNetUsers_user_id",
                table: "order_item",
                column: "user_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_order_item_products_product_id",
                table: "order_item",
                column: "product_id",
                principalTable: "products",
                principalColumn: "product_id");

            migrationBuilder.AddForeignKey(
                name: "FK_order_item_promotion_product_id",
                table: "order_item",
                column: "product_id",
                principalTable: "promotion",
                principalColumn: "promotion_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_item_AspNetUsers_user_id",
                table: "order_item");

            migrationBuilder.DropForeignKey(
                name: "FK_order_item_products_product_id",
                table: "order_item");

            migrationBuilder.DropForeignKey(
                name: "FK_order_item_promotion_product_id",
                table: "order_item");

            migrationBuilder.AlterColumn<string>(
                name: "user_id",
                table: "order_item",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

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
                name: "FK_order_item_AspNetUsers_user_id",
                table: "order_item",
                column: "user_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_order_item_products_product_id",
                table: "order_item",
                column: "product_id",
                principalTable: "products",
                principalColumn: "product_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_order_item_promotion_PromotionId1",
                table: "order_item",
                column: "PromotionId1",
                principalTable: "promotion",
                principalColumn: "promotion_id");
        }
    }
}
