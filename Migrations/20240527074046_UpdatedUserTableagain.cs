using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wepay.Migrations
{
    public partial class UpdatedUserTableagain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3253a28c-aa6c-495c-97fb-cac2788588f5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b9dcebc0-3cd3-4fcd-b2e5-3151d2698479");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c1d01b86-fd83-47d8-9aad-cac5b0071efd");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3253a28c-aa6c-495c-97fb-cac2788588f5", "b18e4ae3-3d7d-421c-bc00-7d23025adb3d", "Elite", "ELITE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b9dcebc0-3cd3-4fcd-b2e5-3151d2698479", "be58eed5-d8fc-4b94-a9b9-1ea2a5dc6435", "Noob", "NOOB" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c1d01b86-fd83-47d8-9aad-cac5b0071efd", "d318ecf0-5c96-4a55-9e3f-7c31ace3f54b", "Admin", "ADMIN" });
        }
    }
}
