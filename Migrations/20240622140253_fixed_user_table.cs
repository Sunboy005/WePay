using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wepay.Migrations
{
    public partial class fixed_user_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "31aeb715-e3b4-4259-b73c-78d3c35ccd28");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a6c70255-7eac-46be-9c9a-da34f7dbbe0f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b65627cd-9010-4471-b536-379caf2e9bb7");

            migrationBuilder.DropColumn(
                name: "User",
                table: "Wallets");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5f32fd1a-83f5-40b4-bc0a-5d0d8f7e9e20", "15d4decf-4885-4367-88cb-0eb9630ae3dc", "Noob", "NOOB" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b44304d2-742d-4d13-ace0-1b3faf07387d", "71d9bc61-be6f-4de9-9f69-8b3f38ef0d01", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d1677cfd-dc6a-4edd-9ed1-05750595cbad", "3d57a3df-415e-477b-93f6-69d6db383fb2", "Elite", "ELITE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5f32fd1a-83f5-40b4-bc0a-5d0d8f7e9e20");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b44304d2-742d-4d13-ace0-1b3faf07387d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d1677cfd-dc6a-4edd-9ed1-05750595cbad");

            migrationBuilder.AddColumn<string>(
                name: "User",
                table: "Wallets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "31aeb715-e3b4-4259-b73c-78d3c35ccd28", "b84ed93b-73f4-48f5-b958-150d3f7df5d2", "Noob", "NOOB" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a6c70255-7eac-46be-9c9a-da34f7dbbe0f", "8f86890e-6cdc-45fa-b007-a2d89be7a346", "Elite", "ELITE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b65627cd-9010-4471-b536-379caf2e9bb7", "fb7adc16-349c-42e1-8783-afa79307ecbe", "Admin", "ADMIN" });
        }
    }
}
