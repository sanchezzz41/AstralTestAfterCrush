using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AstralTest.Migrations
{
    public partial class EditNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_Filess_FileId",
                table: "Attachments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Filess",
                table: "Filess");

            migrationBuilder.RenameTable(
                name: "Filess",
                newName: "Files");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Files",
                table: "Files",
                column: "FileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_Files_FileId",
                table: "Attachments",
                column: "FileId",
                principalTable: "Files",
                principalColumn: "FileId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_Files_FileId",
                table: "Attachments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Files",
                table: "Files");

            migrationBuilder.RenameTable(
                name: "Files",
                newName: "Filess");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Filess",
                table: "Filess",
                column: "FileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_Filess_FileId",
                table: "Attachments",
                column: "FileId",
                principalTable: "Filess",
                principalColumn: "FileId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
