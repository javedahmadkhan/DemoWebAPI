using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Demo.Entities.Migrations
{
    /// <summary>
    /// 
    /// </summary>
    public partial class InitialCreate : Migration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "TodoItem",
                schema: "dbo",
                columns: table => new
                {
                    PKId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    MorningTask = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    AfternoonTask = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    EveningTask = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsTaskComplete = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoItem", x => x.PKId);
                });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TodoItem",
                schema: "dbo");
        }
    }
}
