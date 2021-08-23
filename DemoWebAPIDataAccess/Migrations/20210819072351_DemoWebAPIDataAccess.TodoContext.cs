using Microsoft.EntityFrameworkCore.Migrations;

namespace DemoWebAPIDataAccess.Migrations
{
    public partial class DemoWebAPIDataAccessTodoContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "TodoItem",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    MorningTask = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    AfternoonTask = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    EveningTask = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsTaskComplete = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoItem", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "TodoItem",
                columns: new[] { "Id", "AfternoonTask", "EveningTask", "IsTaskComplete", "MorningTask", "TaskName" },
                values: new object[] { 1, "Lunch", "Dinner", "Y", "Breakfast", "Task1" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "TodoItem",
                columns: new[] { "Id", "AfternoonTask", "EveningTask", "IsTaskComplete", "MorningTask", "TaskName" },
                values: new object[] { 2, "Test", "Deploy", "Y", "Code", "Task2" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TodoItem",
                schema: "dbo");
        }
    }
}
