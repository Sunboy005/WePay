using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wepay.Migrations
{
    public partial class AddedWalletTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "03e2983a-776c-4eee-a945-467b152ffca9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "34f0db62-264c-4817-8a0d-3aaf98c9a1df");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8934ad55-8811-47c3-9149-004f1fc5c5ac");

            migrationBuilder.CreateTable(
                name: "Wallets",
                columns: table => new
                {
                    WalletId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsLocked = table.Column<bool>(type: "bit", nullable: false),
                    Pin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallets", x => x.WalletId);
                    table.ForeignKey(
                        name: "FK_Wallets_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2b6f5bab-91c6-433f-8c27-67785c6f8e3a", "30528196-a78d-44a4-a7c9-6497ec7ca545", "Noob", "NOOB" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "54475e3a-0d10-4f68-ab2d-efba5bfe0222", "534b5864-4805-4369-a2d5-0f3f99a72a7d", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d3476bf6-f42b-44e9-95f6-e4d1e342a90e", "b681d77b-49f3-4cd6-a9ae-ad3282bf6681", "Elite", "ELITE" });

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_Id",
                table: "Wallets",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Wallets");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2b6f5bab-91c6-433f-8c27-67785c6f8e3a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "54475e3a-0d10-4f68-ab2d-efba5bfe0222");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3476bf6-f42b-44e9-95f6-e4d1e342a90e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "03e2983a-776c-4eee-a945-467b152ffca9", "c8db0083-e83c-438a-b324-a7972cab9300", "Elite", "ELITE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "34f0db62-264c-4817-8a0d-3aaf98c9a1df", "446ecc25-17e8-4e80-826a-75de4e8e7de7", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8934ad55-8811-47c3-9149-004f1fc5c5ac", "4779dab6-c0fd-48bf-8b2e-7c672734b55c", "Noob", "NOOB" });
        }
    }
}
