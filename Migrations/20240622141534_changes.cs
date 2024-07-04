using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wepay.Migrations
{
    public partial class changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "00cd3b7f-9185-41bb-8953-5c301160f997", "27ae3b70-224a-4fdd-8b7b-892b1872014a", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7f4bbd44-41b0-4696-8ebf-a6fbca23b7fe", "f439034f-6a6a-441d-8cbf-dd27722a33e8", "Noob", "NOOB" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d87955f7-db45-4ad8-a342-5209a216ce67", "3536d12b-7de0-431b-b804-59365fcc6e1d", "Elite", "ELITE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "00cd3b7f-9185-41bb-8953-5c301160f997");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7f4bbd44-41b0-4696-8ebf-a6fbca23b7fe");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d87955f7-db45-4ad8-a342-5209a216ce67");

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
    }
}
