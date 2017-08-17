using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using AstralTest.Database;
using AstralTest.Domain.Entities;

namespace AstralTest.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20170813151102_AddEnteredUsers")]
    partial class AddEnteredUsers
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("AstralTest.Domain.Entities.Attachment", b =>
                {
                    b.Property<Guid>("AttachmentId");

                    b.Property<Guid>("FileId");

                    b.Property<Guid>("TaskId");

                    b.HasKey("AttachmentId");

                    b.HasIndex("FileId");

                    b.HasIndex("TaskId");

                    b.ToTable("Attachments");
                });

            modelBuilder.Entity("AstralTest.Domain.Entities.EnteredUser", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<Guid>("IdUser");

                    b.HasKey("Id");

                    b.HasIndex("IdUser");

                    b.ToTable("EnteredUsers");
                });

            modelBuilder.Entity("AstralTest.Domain.Entities.File", b =>
                {
                    b.Property<Guid>("FileId");

                    b.Property<DateTime>("CreatedTime");

                    b.Property<string>("NameFile")
                        .IsRequired();

                    b.Property<string>("TypeFile")
                        .IsRequired();

                    b.HasKey("FileId");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("AstralTest.Domain.Entities.InfoAboutEnteredUser", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<DateTime>("EnteredTime");

                    b.Property<Guid>("IdEnteredUser");

                    b.Property<string>("NameOfAction")
                        .IsRequired();

                    b.Property<string>("NameOfController")
                        .IsRequired();

                    b.Property<string>("ParametrsToAction");

                    b.HasKey("Id");

                    b.HasIndex("IdEnteredUser");

                    b.ToTable("InfoAboutEnteredUsers");
                });

            modelBuilder.Entity("AstralTest.Domain.Entities.Note", b =>
                {
                    b.Property<Guid>("NoteId");

                    b.Property<Guid>("IdUser");

                    b.Property<string>("Text")
                        .IsRequired();

                    b.HasKey("NoteId");

                    b.HasIndex("IdUser");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("AstralTest.Domain.Entities.Role", b =>
                {
                    b.Property<int>("RoleId");

                    b.Property<string>("RoleName");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("AstralTest.Domain.Entities.TasksContainer", b =>
                {
                    b.Property<Guid>("ListId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<Guid>("UserId");

                    b.HasKey("ListId");

                    b.HasIndex("UserId");

                    b.ToTable("TasksContainers");
                });

            modelBuilder.Entity("AstralTest.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("PasswordHash")
                        .IsRequired();

                    b.Property<string>("PasswordSalt");

                    b.Property<int>("RoleId");

                    b.Property<string>("UserName")
                        .IsRequired();

                    b.HasKey("UserId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AstralTest.Domain.Entities.UserTask", b =>
                {
                    b.Property<Guid>("TaskId");

                    b.Property<DateTime>("ActualTimeEnd");

                    b.Property<DateTime>("EndTime");

                    b.Property<Guid>("ListId");

                    b.Property<string>("TextTask")
                        .IsRequired();

                    b.HasKey("TaskId");

                    b.HasIndex("ListId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("AstralTest.Domain.Entities.Attachment", b =>
                {
                    b.HasOne("AstralTest.Domain.Entities.File", "MasterFile")
                        .WithMany()
                        .HasForeignKey("FileId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AstralTest.Domain.Entities.UserTask", "MasterTask")
                        .WithMany("Attachments")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AstralTest.Domain.Entities.EnteredUser", b =>
                {
                    b.HasOne("AstralTest.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AstralTest.Domain.Entities.InfoAboutEnteredUser", b =>
                {
                    b.HasOne("AstralTest.Domain.Entities.EnteredUser", "EnteredUser")
                        .WithMany("InfoAboutEnteredUsers")
                        .HasForeignKey("IdEnteredUser")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AstralTest.Domain.Entities.Note", b =>
                {
                    b.HasOne("AstralTest.Domain.Entities.User", "Master")
                        .WithMany("Notes")
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AstralTest.Domain.Entities.TasksContainer", b =>
                {
                    b.HasOne("AstralTest.Domain.Entities.User", "Master")
                        .WithMany("TasksContainers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AstralTest.Domain.Entities.User", b =>
                {
                    b.HasOne("AstralTest.Domain.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AstralTest.Domain.Entities.UserTask", b =>
                {
                    b.HasOne("AstralTest.Domain.Entities.TasksContainer", "MasterList")
                        .WithMany("Tasks")
                        .HasForeignKey("ListId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
