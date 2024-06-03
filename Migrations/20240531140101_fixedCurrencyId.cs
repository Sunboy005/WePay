using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wepay.Migrations
{
    public partial class fixedCurrencyId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4783dec3-dfe7-4f20-9873-d7a6807476ca");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "53220965-0e1c-471c-8808-a38f8c9a4d73");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8c80a996-4336-474d-b7b0-bdde84e22597");

            migrationBuilder.RenameColumn(
                name: "Id1",
                table: "Currencies",
                newName: "CurrencyId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0faff75b-44a6-40ea-96fc-d6f8e3888a4c", "32b73c8e-8ec3-4c87-a54c-a97868629ff4", "Noob", "NOOB" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2a4dae23-5dad-40fe-80ff-3cfbdfd05c76", "1bd04689-65e7-4be0-81da-beb2720eb6db", "Elite", "ELITE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5364f146-eb37-43d0-8418-b17491731242", "6f317d99-3bf0-4e8d-ba59-40f97f0eb958", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0faff75b-44a6-40ea-96fc-d6f8e3888a4c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2a4dae23-5dad-40fe-80ff-3cfbdfd05c76");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5364f146-eb37-43d0-8418-b17491731242");

            migrationBuilder.RenameColumn(
                name: "CurrencyId",
                table: "Currencies",
                newName: "Id1");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4783dec3-dfe7-4f20-9873-d7a6807476ca", "12a8a856-8e9c-4205-8135-7dd9d8a7128c", "Elite", "ELITE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "53220965-0e1c-471c-8808-a38f8c9a4d73", "9e55a9e7-7ed7-454d-a588-9f12f2e95c53", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8c80a996-4336-474d-b7b0-bdde84e22597", "11b2b2a7-b1d6-4701-adf4-3f2591241e5c", "Noob", "NOOB" });
        }
    }
}
