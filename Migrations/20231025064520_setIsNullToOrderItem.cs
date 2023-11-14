using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class setIsNullToOrderItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Promotions_product_id",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Promotions_promotion_id",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_orders_order_id",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_products_product_id",
                table: "OrderItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Promotions",
                table: "Promotions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderItems",
                table: "OrderItems");

            migrationBuilder.RenameTable(
                name: "Promotions",
                newName: "promotion");

            migrationBuilder.RenameTable(
                name: "OrderItems",
                newName: "order_item");

            migrationBuilder.RenameIndex(
                name: "IX_Promotions_code",
                table: "promotion",
                newName: "IX_promotion_code");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_promotion_id",
                table: "order_item",
                newName: "IX_order_item_promotion_id");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_product_id",
                table: "order_item",
                newName: "IX_order_item_product_id");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_order_id",
                table: "order_item",
                newName: "IX_order_item_order_id");

            migrationBuilder.AlterColumn<int>(
                name: "promotion_id",
                table: "order_item",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "order_id",
                table: "order_item",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_promotion",
                table: "promotion",
                column: "promotion_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_order_item",
                table: "order_item",
                column: "order_item_id");

            migrationBuilder.AddForeignKey(
                name: "FK_order_item_orders_order_id",
                table: "order_item",
                column: "order_id",
                principalTable: "orders",
                principalColumn: "order_id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_order_item_promotion_promotion_id",
                table: "order_item",
                column: "promotion_id",
                principalTable: "promotion",
                principalColumn: "promotion_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_item_orders_order_id",
                table: "order_item");

            migrationBuilder.DropForeignKey(
                name: "FK_order_item_products_product_id",
                table: "order_item");

            migrationBuilder.DropForeignKey(
                name: "FK_order_item_promotion_product_id",
                table: "order_item");

            migrationBuilder.DropForeignKey(
                name: "FK_order_item_promotion_promotion_id",
                table: "order_item");

            migrationBuilder.DropPrimaryKey(
                name: "PK_promotion",
                table: "promotion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_order_item",
                table: "order_item");

            migrationBuilder.RenameTable(
                name: "promotion",
                newName: "Promotions");

            migrationBuilder.RenameTable(
                name: "order_item",
                newName: "OrderItems");

            migrationBuilder.RenameIndex(
                name: "IX_promotion_code",
                table: "Promotions",
                newName: "IX_Promotions_code");

            migrationBuilder.RenameIndex(
                name: "IX_order_item_promotion_id",
                table: "OrderItems",
                newName: "IX_OrderItems_promotion_id");

            migrationBuilder.RenameIndex(
                name: "IX_order_item_product_id",
                table: "OrderItems",
                newName: "IX_OrderItems_product_id");

            migrationBuilder.RenameIndex(
                name: "IX_order_item_order_id",
                table: "OrderItems",
                newName: "IX_OrderItems_order_id");

            migrationBuilder.AlterColumn<int>(
                name: "promotion_id",
                table: "OrderItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "order_id",
                table: "OrderItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Promotions",
                table: "Promotions",
                column: "promotion_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderItems",
                table: "OrderItems",
                column: "order_item_id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Promotions_product_id",
                table: "OrderItems",
                column: "product_id",
                principalTable: "Promotions",
                principalColumn: "promotion_id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Promotions_promotion_id",
                table: "OrderItems",
                column: "promotion_id",
                principalTable: "Promotions",
                principalColumn: "promotion_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_orders_order_id",
                table: "OrderItems",
                column: "order_id",
                principalTable: "orders",
                principalColumn: "order_id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_products_product_id",
                table: "OrderItems",
                column: "product_id",
                principalTable: "products",
                principalColumn: "product_id");
        }
    }
}
