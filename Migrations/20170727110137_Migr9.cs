using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AstralTest.Migrations
{
    public partial class Migr9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyTasks_ListOfTasks_ListId",
                table: "MyTasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MyTasks",
                table: "MyTasks");

            migrationBuilder.RenameTable(
                name: "MyTasks",
                newName: "Tasks");

            migrationBuilder.RenameIndex(
                name: "IX_MyTasks_ListId",
                table: "Tasks",
                newName: "IX_Tasks_ListId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_ListOfTasks_ListId",
                table: "Tasks",
                column: "ListId",
                principalTable: "ListOfTasks",
                principalColumn: "ListId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_ListOfTasks_ListId",
                table: "Tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks");

            migrationBuilder.RenameTable(
                name: "Tasks",
                newName: "MyTasks");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_ListId",
                table: "MyTasks",
                newName: "IX_MyTasks_ListId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MyTasks",
                table: "MyTasks",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_MyTasks_ListOfTasks_ListId",
                table: "MyTasks",
                column: "ListId",
                principalTable: "ListOfTasks",
                principalColumn: "ListId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
