using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaxBackApp.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskCommentTypes",
                columns: table => new
                {
                    TaskCommentTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TaskCommentTypeDescription = table.Column<string>(type: "nvarchar(250)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskCommentTypes", x => x.TaskCommentTypeId);
                });

            migrationBuilder.CreateTable(
                name: "TaskStatuses",
                columns: table => new
                {
                    TaskStatusId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TaskStatusDescription = table.Column<string>(type: "nvarchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskStatuses", x => x.TaskStatusId);
                });

            migrationBuilder.CreateTable(
                name: "TaskTypes",
                columns: table => new
                {
                    TaskTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TaskTypeDescription = table.Column<string>(type: "nvarchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskTypes", x => x.TaskTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "TaskEntries",
                columns: table => new
                {
                    TaskEntryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTimeOffset>(nullable: false),
                    DateDue = table.Column<DateTimeOffset>(nullable: false),
                    TaskEntryDescription = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    TaskStatusId = table.Column<int>(nullable: false),
                    TaskTypeId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: true),
                    DateNextAction = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskEntries", x => x.TaskEntryId);
                    table.ForeignKey(
                        name: "FK_TaskEntries_TaskStatuses_TaskStatusId",
                        column: x => x.TaskStatusId,
                        principalTable: "TaskStatuses",
                        principalColumn: "TaskStatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskEntries_TaskTypes_TaskTypeId",
                        column: x => x.TaskTypeId,
                        principalTable: "TaskTypes",
                        principalColumn: "TaskTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskEntries_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "TaskComments",
                columns: table => new
                {
                    TaskCommentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TaskCommentDescription = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    TaskCommentTypeId = table.Column<int>(nullable: false),
                    ReminderDate = table.Column<DateTimeOffset>(nullable: false),
                    TaskEntryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskComments", x => x.TaskCommentId);
                    table.ForeignKey(
                        name: "FK_TaskComments_TaskCommentTypes_TaskCommentTypeId",
                        column: x => x.TaskCommentTypeId,
                        principalTable: "TaskCommentTypes",
                        principalColumn: "TaskCommentTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskComments_TaskEntries_TaskEntryId",
                        column: x => x.TaskEntryId,
                        principalTable: "TaskEntries",
                        principalColumn: "TaskEntryId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskComments_TaskCommentTypeId",
                table: "TaskComments",
                column: "TaskCommentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskComments_TaskEntryId",
                table: "TaskComments",
                column: "TaskEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskEntries_TaskStatusId",
                table: "TaskEntries",
                column: "TaskStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskEntries_TaskTypeId",
                table: "TaskEntries",
                column: "TaskTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskEntries_UserId",
                table: "TaskEntries",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskComments");

            migrationBuilder.DropTable(
                name: "TaskCommentTypes");

            migrationBuilder.DropTable(
                name: "TaskEntries");

            migrationBuilder.DropTable(
                name: "TaskStatuses");

            migrationBuilder.DropTable(
                name: "TaskTypes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
