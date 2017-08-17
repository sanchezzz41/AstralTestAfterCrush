using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AstralTest.Migrations
{
    public partial class Migr10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_ListOfTasks_ListId",
                table: "Tasks");

            migrationBuilder.DropTable(
                name: "ListOfTasks");

            migrationBuilder.CreateTable(
                name: "TasksContainers",
                columns: table => new
                {
                    ListId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TasksContainers", x => x.ListId);
                    table.ForeignKey(
                        name: "FK_TasksContainers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TasksContainers_UserId",
                table: "TasksContainers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_TasksContainers_ListId",
                table: "Tasks",
                column: "ListId",
                principalTable: "TasksContainers",
                principalColumn: "ListId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_TasksContainers_ListId",
                table: "Tasks");

            migrationBuilder.DropTable(
                name: "TasksContainers");

            migrationBuilder.CreateTable(
                name: "ListOfTasks",
                columns: table => new
                {
                    ListId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListOfTasks", x => x.ListId);
                    table.ForeignKey(
                        name: "FK_ListOfTasks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ListOfTasks_UserId",
                table: "ListOfTasks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_ListOfTasks_ListId",
                table: "Tasks",
                column: "ListId",
                principalTable: "ListOfTasks",
                principalColumn: "ListId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
