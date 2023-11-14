using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class delete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_item_promotion_PromotionId1",
                table: "order_item");

            migrationBuilder.DropForeignKey(
                name: "FK_order_item_promotion_promotion_id",
                table: "order_item");

            migrationBuilder.DropIndex(
                name: "IX_order_item_PromotionId1",
                table: "order_item");

            migrationBuilder.DropColumn(
                name: "PromotionId1",
                table: "order_item");

            migrationBuilder.RenameColumn(
                name: "promotion_id",
                table: "order_item",
                newName: "PromotionId");

            migrationBuilder.RenameIndex(
                name: "IX_order_item_promotion_id",
                table: "order_item",
                newName: "IX_order_item_PromotionId");

            migrationBuilder.AlterColumn<int>(
                name: "PromotionId",
                table: "order_item",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_order_item_promotion_PromotionId",
                table: "order_item",
                column: "PromotionId",
                principalTable: "promotion",
                principalColumn: "promotion_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_item_promotion_PromotionId",
                table: "order_item");

            migrationBuilder.RenameColumn(
                name: "PromotionId",
                table: "order_item",
                newName: "promotion_id");

            migrationBuilder.RenameIndex(
                name: "IX_order_item_PromotionId",
                table: "order_item",
                newName: "IX_order_item_promotion_id");

            migrationBuilder.AlterColumn<int>(
                name: "promotion_id",
                table: "order_item",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
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
                name: "FK_order_item_promotion_PromotionId1",
                table: "order_item",
                column: "PromotionId1",
                principalTable: "promotion",
                principalColumn: "promotion_id");

            migrationBuilder.AddForeignKey(
                name: "FK_order_item_promotion_promotion_id",
                table: "order_item",
                column: "promotion_id",
                principalTable: "promotion",
                principalColumn: "promotion_id");
        }
    }
}
