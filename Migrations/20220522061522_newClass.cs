using Microsoft.EntityFrameworkCore.Migrations;

namespace WeatherApp.Migrations
{
    public partial class newClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MyClasses",
                table: "MyClasses");

            migrationBuilder.DropIndex(
                name: "IX_MyClasses_StudentId",
                table: "MyClasses");

            migrationBuilder.DropColumn(
                name: "ClassId",
                table: "MyClasses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MyClasses",
                table: "MyClasses",
                columns: new[] { "StudentId", "TeacherId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MyClasses",
                table: "MyClasses");

            migrationBuilder.AddColumn<int>(
                name: "ClassId",
                table: "MyClasses",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MyClasses",
                table: "MyClasses",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_MyClasses_StudentId",
                table: "MyClasses",
                column: "StudentId");
        }
    }
}
