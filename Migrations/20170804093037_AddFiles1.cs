using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AstralTest.Migrations
{
    public partial class AddFiles1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Notes",
                newName: "NoteId");

            migrationBuilder.RenameColumn(
                name: "FIleId",
                table: "Files",
                newName: "FileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NoteId",
                table: "Notes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "FileId",
                table: "Files",
                newName: "FIleId");
        }
    }
}
