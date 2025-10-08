using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TNTU.ToDoApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDoItems_Users_UserId",
                table: "ToDoItems");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ToDoItems",
                newName: "GreatUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ToDoItems_UserId",
                table: "ToDoItems",
                newName: "IX_ToDoItems_GreatUserId");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ToDoItems",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ToDoItems",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoItems_Users_GreatUserId",
                table: "ToDoItems",
                column: "GreatUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDoItems_Users_GreatUserId",
                table: "ToDoItems");

            migrationBuilder.RenameColumn(
                name: "GreatUserId",
                table: "ToDoItems",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ToDoItems_GreatUserId",
                table: "ToDoItems",
                newName: "IX_ToDoItems_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ToDoItems",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ToDoItems",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoItems_Users_UserId",
                table: "ToDoItems",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
