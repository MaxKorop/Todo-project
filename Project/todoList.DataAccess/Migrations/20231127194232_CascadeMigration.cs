using Microsoft.EntityFrameworkCore.Migrations;

namespace todoList.DataAccess.Migrations
{
    public partial class CascadeMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskLists_Users_CreatedById",
                table: "TaskLists");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Priorities_PriorityId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Statuses_StatusId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_TaskLists_TListId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Users_CreatedById",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersLists_TaskLists_TListId",
                table: "UsersLists");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersLists_Users_UserId",
                table: "UsersLists");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersTasks_Tasks_TaskId",
                table: "UsersTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersTasks_Users_UserId",
                table: "UsersTasks");

            migrationBuilder.AlterColumn<int>(
                name: "CreatedById",
                table: "Tasks",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskLists_Users_CreatedById",
                table: "TaskLists",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Priorities_PriorityId",
                table: "Tasks",
                column: "PriorityId",
                principalTable: "Priorities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Statuses_StatusId",
                table: "Tasks",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_TaskLists_TListId",
                table: "Tasks",
                column: "TListId",
                principalTable: "TaskLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Users_CreatedById",
                table: "Tasks",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersLists_TaskLists_TListId",
                table: "UsersLists",
                column: "TListId",
                principalTable: "TaskLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersLists_Users_UserId",
                table: "UsersLists",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersTasks_Tasks_TaskId",
                table: "UsersTasks",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersTasks_Users_UserId",
                table: "UsersTasks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskLists_Users_CreatedById",
                table: "TaskLists");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Priorities_PriorityId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Statuses_StatusId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_TaskLists_TListId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Users_CreatedById",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersLists_TaskLists_TListId",
                table: "UsersLists");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersLists_Users_UserId",
                table: "UsersLists");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersTasks_Tasks_TaskId",
                table: "UsersTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersTasks_Users_UserId",
                table: "UsersTasks");

            migrationBuilder.AlterColumn<int>(
                name: "CreatedById",
                table: "Tasks",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskLists_Users_CreatedById",
                table: "TaskLists",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Priorities_PriorityId",
                table: "Tasks",
                column: "PriorityId",
                principalTable: "Priorities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Statuses_StatusId",
                table: "Tasks",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_TaskLists_TListId",
                table: "Tasks",
                column: "TListId",
                principalTable: "TaskLists",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Users_CreatedById",
                table: "Tasks",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersLists_TaskLists_TListId",
                table: "UsersLists",
                column: "TListId",
                principalTable: "TaskLists",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersLists_Users_UserId",
                table: "UsersLists",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersTasks_Tasks_TaskId",
                table: "UsersTasks",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersTasks_Users_UserId",
                table: "UsersTasks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
