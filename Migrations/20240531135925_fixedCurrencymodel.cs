using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wepay.Migrations
{
    public partial class fixedCurrencymodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "413a434c-8658-4066-9550-0c63f2fc96d8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "80bec221-c2be-4333-a0cd-050cfc311e27");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ea5e746b-2ca4-47ad-861d-c43a9528d3c3");

            migrationBuilder.RenameColumn(
                name: "CurrencyName",
                table: "Currencies",
                newName: "Name");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "Name",
                table: "Currencies",
                newName: "CurrencyName");

            migrationBuilder.RenameColumn(
                name: "Id1",
                table: "Currencies",
                newName: "CurrencyId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "413a434c-8658-4066-9550-0c63f2fc96d8", "d1bb05ca-8e0e-4e1b-8662-60eb7c16449e", "Noob", "NOOB" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "80bec221-c2be-4333-a0cd-050cfc311e27", "572f42d6-3a89-48a1-bd70-9545cd2f3180", "Elite", "ELITE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ea5e746b-2ca4-47ad-861d-c43a9528d3c3", "dd65a02c-4957-48f1-b91f-49997a2e16d1", "Admin", "ADMIN" });
        }
    }
}
