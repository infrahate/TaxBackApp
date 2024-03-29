﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaxBackApp.Models;

namespace TaxBackApp.Migrations
{
    [DbContext(typeof(TaxBackContext))]
    partial class TaxBackContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TaxBackApp.Models.TaskComment", b =>
                {
                    b.Property<int>("TaskCommentId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("ReminderDate");

                    b.Property<string>("TaskCommentDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("TaskCommentTypeId");

                    b.Property<int?>("TaskEntryId");

                    b.HasKey("TaskCommentId");

                    b.HasIndex("TaskCommentTypeId");

                    b.HasIndex("TaskEntryId");

                    b.ToTable("TaskComments");
                });

            modelBuilder.Entity("TaxBackApp.Models.TaskCommentType", b =>
                {
                    b.Property<int>("TaskCommentTypeId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("TaskCommentTypeDescription")
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("TaskCommentTypeId");

                    b.ToTable("TaskCommentTypes");
                });

            modelBuilder.Entity("TaxBackApp.Models.TaskEntry", b =>
                {
                    b.Property<int>("TaskEntryId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("DateCreated");

                    b.Property<DateTimeOffset>("DateDue");

                    b.Property<DateTimeOffset>("DateNextAction");

                    b.Property<string>("TaskEntryDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("TaskStatusId");

                    b.Property<int>("TaskTypeId");

                    b.Property<int?>("UserId");

                    b.HasKey("TaskEntryId");

                    b.HasIndex("TaskStatusId");

                    b.HasIndex("TaskTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("TaskEntries");
                });

            modelBuilder.Entity("TaxBackApp.Models.TaskStatus", b =>
                {
                    b.Property<int>("TaskStatusId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("TaskStatusDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("TaskStatusId");

                    b.ToTable("TaskStatuses");
                });

            modelBuilder.Entity("TaxBackApp.Models.TaskType", b =>
                {
                    b.Property<int>("TaskTypeId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("TaskTypeDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("TaskTypeId");

                    b.ToTable("TaskTypes");
                });

            modelBuilder.Entity("TaxBackApp.Models.UserList", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TaxBackApp.Models.TaskComment", b =>
                {
                    b.HasOne("TaxBackApp.Models.TaskCommentType", "TaskCommentType")
                        .WithMany("TaskComments")
                        .HasForeignKey("TaskCommentTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TaxBackApp.Models.TaskEntry", "TaskEntry")
                        .WithMany("TaskComments")
                        .HasForeignKey("TaskEntryId");
                });

            modelBuilder.Entity("TaxBackApp.Models.TaskEntry", b =>
                {
                    b.HasOne("TaxBackApp.Models.TaskStatus", "TaskStatus")
                        .WithMany("TaskEntries")
                        .HasForeignKey("TaskStatusId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TaxBackApp.Models.TaskType", "TaskType")
                        .WithMany("TaskEntries")
                        .HasForeignKey("TaskTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TaxBackApp.Models.UserList", "UserList")
                        .WithMany("TaskEntries")
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
