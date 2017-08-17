using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AstralTest.Migrations
{
    public partial class AddPkAttachments4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Attachments",
                table: "Attachments");

            migrationBuilder.AddColumn<Guid>(
                name: "AttachmentId",
                table: "Attachments",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Attachments",
                table: "Attachments",
                column: "AttachmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_FileId",
                table: "Attachments",
                column: "FileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Attachments",
                table: "Attachments");

            migrationBuilder.DropIndex(
                name: "IX_Attachments_FileId",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "AttachmentId",
                table: "Attachments");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Attachments",
                table: "Attachments",
                columns: new[] { "FileId", "TaskId" });
        }
    }
}
