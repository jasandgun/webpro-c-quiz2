using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JAKList.Data.Migrations
{
    public partial class AdminAddition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "68667585-c95d-4767-bab0-b7aae4b65502", "54a72e5f-8368-4317-8e34-e3bf43267c30", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "68667585-c95d-4767-bab0-b7aae4b65502", "07307a26-d20f-493e-b42a-c75636c141c9" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "68667585-c95d-4767-bab0-b7aae4b65502", "07307a26-d20f-493e-b42a-c75636c141c9" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "68667585-c95d-4767-bab0-b7aae4b65502");
        }
    }
}
