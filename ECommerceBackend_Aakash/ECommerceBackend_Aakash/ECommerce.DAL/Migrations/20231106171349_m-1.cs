﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.DAL.Migrations
{
    public partial class m1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Carts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Carts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "35403dae-8769-4773-b617-a5087d827284",
                column: "ConcurrencyStamp",
                value: "52b5ffc1-a6c2-4746-90d4-4e916c62f706");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "649fca11-51e3-4db8-91d7-0bba63163280",
                column: "ConcurrencyStamp",
                value: "12b04876-18c4-4cbd-af3f-26820157356f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "422b7d11-6c54-436e-a04f-7619e7acf637",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "79c76d7a-c1d5-42c7-9846-515a85d2c1b1", "AQAAAAEAACcQAAAAEGhZ2dWi1nvI2XX4uEHwA+xkogNcQALuA89eWHS7p+kNpc4inwE31my3N/eVrtSkUg==", "96e5e6af-79a9-4fc6-83ba-a57479f0806c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5eedf264-7629-412a-937e-926ec371be3c",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "99284187-9dd0-4388-91b4-8d86631f6b2e", "AQAAAAEAACcQAAAAEMt5VPs1P8HbUchNcT5xCjb3Lw4KkxWcqa9cJWH4Rca9ImhWUdrijiOjKGI3s4i6eg==", "94190433-17c9-4011-8e90-52c8762bc70e" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1,
                columns: new[] { "CreatedOn", "UpdatedOn" },
                values: new object[] { new DateTime(2023, 11, 6, 22, 43, 49, 372, DateTimeKind.Local).AddTicks(3579), new DateTime(2023, 11, 6, 22, 43, 49, 372, DateTimeKind.Local).AddTicks(3590) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2,
                columns: new[] { "CreatedOn", "UpdatedOn" },
                values: new object[] { new DateTime(2023, 11, 6, 22, 43, 49, 372, DateTimeKind.Local).AddTicks(3591), new DateTime(2023, 11, 6, 22, 43, 49, 372, DateTimeKind.Local).AddTicks(3592) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3,
                columns: new[] { "CreatedOn", "UpdatedOn" },
                values: new object[] { new DateTime(2023, 11, 6, 22, 43, 49, 372, DateTimeKind.Local).AddTicks(3593), new DateTime(2023, 11, 6, 22, 43, 49, 372, DateTimeKind.Local).AddTicks(3593) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "Image",
                value: "https://store.storeimages.cdn-apple.com/4982/as-images.apple.com/is/refurb-iphone-13-pro-max-blue-2023?wid=1144&hei=1144&fmt=jpeg&qlt=90&.v=1679072989205");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                column: "Image",
                value: "https://www.jagranimages.com/images/newimg/26122022/26_12_2022-best_nike_shoes_in_india_23272173.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                columns: new[] { "Image", "Quantity" },
                values: new object[] { "https://www.wildstone.in/cdn/shop/files/ultra100ml_1_grande.jpg?v=1695879969", 0 });

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ProductId",
                table: "Carts",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Products_ProductId",
                table: "Carts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Products_ProductId",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_ProductId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Carts");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "35403dae-8769-4773-b617-a5087d827284",
                column: "ConcurrencyStamp",
                value: "83c98c22-8731-402b-bd0e-59b4649c44fd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "649fca11-51e3-4db8-91d7-0bba63163280",
                column: "ConcurrencyStamp",
                value: "62a5400b-8def-4064-928d-26b0b4b8d9bc");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "422b7d11-6c54-436e-a04f-7619e7acf637",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e241fb3e-4710-4e1e-bcfc-d6bb5e58e3b1", "AQAAAAEAACcQAAAAEDI9o1SU2mgAzw7igRhm+qsKwIRQmrOmaYgPyCNq7JPkeBNqmdK8K/Df+rckHa0HXQ==", "f2c209d4-6a43-48ca-9bfa-83bda811ceb2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5eedf264-7629-412a-937e-926ec371be3c",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "21b2107d-1c4c-4adf-88a5-cb0853012fd9", "AQAAAAEAACcQAAAAENA/VS6zHEs1BOwO4sPKHchORW5XJ4k+EJaJkMVW4EOG7x/F2ICaiyMeSmaC8XxJqA==", "fc03156b-82ae-4281-b4c0-f183ff2ea5fa" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1,
                columns: new[] { "CreatedOn", "UpdatedOn" },
                values: new object[] { new DateTime(2023, 11, 6, 14, 7, 33, 802, DateTimeKind.Local).AddTicks(2582), new DateTime(2023, 11, 6, 14, 7, 33, 802, DateTimeKind.Local).AddTicks(2595) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2,
                columns: new[] { "CreatedOn", "UpdatedOn" },
                values: new object[] { new DateTime(2023, 11, 6, 14, 7, 33, 802, DateTimeKind.Local).AddTicks(2596), new DateTime(2023, 11, 6, 14, 7, 33, 802, DateTimeKind.Local).AddTicks(2597) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3,
                columns: new[] { "CreatedOn", "UpdatedOn" },
                values: new object[] { new DateTime(2023, 11, 6, 14, 7, 33, 802, DateTimeKind.Local).AddTicks(2598), new DateTime(2023, 11, 6, 14, 7, 33, 802, DateTimeKind.Local).AddTicks(2598) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "Image",
                value: "data:image/jpeg;base64,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");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                column: "Image",
                value: "data:image/jpeg;base64,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");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                columns: new[] { "Image", "Quantity" },
                values: new object[] { "data:image/jpeg;base64,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", 10 });
        }
    }
}
