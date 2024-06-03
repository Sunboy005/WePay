using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wepay.Migrations
{
    public partial class fixedForeignKeyForWalletIdInCurrencyTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Currencies_Wallets_Id",
                table: "Currencies");

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
                name: "Id",
                table: "Currencies",
                newName: "WalletId");

            migrationBuilder.RenameIndex(
                name: "IX_Currencies_Id",
                table: "Currencies",
                newName: "IX_Currencies_WalletId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "219c5aac-342c-4e53-a8fd-48678e10ea8b", "3c787017-930c-48a9-85b8-52e5d3e85819", "Elite", "ELITE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "353a1bff-693a-477e-a0b9-8846c281e3d6", "71b960f6-41b2-4e3a-be76-19d8adb81d2a", "Noob", "NOOB" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "af287770-3810-4bd1-bfdc-52d94c42cad0", "c4c2a591-5790-4da0-a88f-40f6477a5961", "Admin", "ADMIN" });

            migrationBuilder.AddForeignKey(
                name: "FK_Currencies_Wallets_WalletId",
                table: "Currencies",
                column: "WalletId",
                principalTable: "Wallets",
                principalColumn: "WalletId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Currencies_Wallets_WalletId",
                table: "Currencies");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "219c5aac-342c-4e53-a8fd-48678e10ea8b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "353a1bff-693a-477e-a0b9-8846c281e3d6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "af287770-3810-4bd1-bfdc-52d94c42cad0");

            migrationBuilder.RenameColumn(
                name: "WalletId",
                table: "Currencies",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Currencies_WalletId",
                table: "Currencies",
                newName: "IX_Currencies_Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Currencies_Wallets_Id",
                table: "Currencies",
                column: "Id",
                principalTable: "Wallets",
                principalColumn: "WalletId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
