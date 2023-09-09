using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DB.Migrations
{
    /// <inheritdoc />
    public partial class addusertasklogtablechangesonusertaskandtasktable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "UserTasks");

            migrationBuilder.DropColumn(
                name: "TaskStatus",
                table: "UserTasks");

            migrationBuilder.AddColumn<bool>(
                name: "TaskStatus",
                table: "Tasks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "UserTaskLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserTaskId = table.Column<int>(type: "int", nullable: false),
                    FunctorId = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTaskLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTaskLogs_UserTasks_UserTaskId",
                        column: x => x.UserTaskId,
                        principalTable: "UserTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_UserTaskLogs_Users_FunctorId",
                        column: x => x.FunctorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserTaskLogs_FunctorId",
                table: "UserTaskLogs",
                column: "FunctorId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTaskLogs_UserTaskId",
                table: "UserTaskLogs",
                column: "UserTaskId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserTaskLogs");

            migrationBuilder.DropColumn(
                name: "TaskStatus",
                table: "Tasks");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "UserTasks",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TaskStatus",
                table: "UserTasks",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
