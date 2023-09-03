using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DB.Migrations
{
    /// <inheritdoc />
    public partial class changeontaskcategorytable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskCategories_Tasks_ProjectTaskId",
                table: "TaskCategories");

            migrationBuilder.DropIndex(
                name: "IX_TaskCategories_ProjectTaskId",
                table: "TaskCategories");

            migrationBuilder.DropColumn(
                name: "ProjectTaskId",
                table: "TaskCategories");

            migrationBuilder.CreateIndex(
                name: "IX_TaskCategories_TaskId",
                table: "TaskCategories",
                column: "TaskId");

            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.DropIndex(
                name: "IX_TaskCategories_TaskId",
                table: "TaskCategories");

            migrationBuilder.AddColumn<int>(
                name: "ProjectTaskId",
                table: "TaskCategories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskCategories_ProjectTaskId",
                table: "TaskCategories",
                column: "ProjectTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskCategories_Tasks_ProjectTaskId",
                table: "TaskCategories",
                column: "ProjectTaskId",
                principalTable: "Tasks",
                principalColumn: "Id");
        }
    }
}
