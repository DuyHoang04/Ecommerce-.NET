using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class CreateRelationshipOrderwithUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ratings_AspNetUsers_user_id",
                table: "ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_ratings_products_product_id",
                table: "ratings");

            migrationBuilder.AlterColumn<string>(
                name: "user_id",
                table: "ratings",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "user_id",
                table: "order_item",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_order_item_user_id",
                table: "order_item",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_order_item_AspNetUsers_user_id",
                table: "order_item",
                column: "user_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ratings_AspNetUsers_user_id",
                table: "ratings",
                column: "user_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ratings_products_product_id",
                table: "ratings",
                column: "product_id",
                principalTable: "products",
                principalColumn: "product_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_item_AspNetUsers_user_id",
                table: "order_item");

            migrationBuilder.DropForeignKey(
                name: "FK_ratings_AspNetUsers_user_id",
                table: "ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_ratings_products_product_id",
                table: "ratings");

            migrationBuilder.DropIndex(
                name: "IX_order_item_user_id",
                table: "order_item");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "order_item");

            migrationBuilder.AlterColumn<string>(
                name: "user_id",
                table: "ratings",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_ratings_AspNetUsers_user_id",
                table: "ratings",
                column: "user_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ratings_products_product_id",
                table: "ratings",
                column: "product_id",
                principalTable: "products",
                principalColumn: "product_id");
        }
    }
}
