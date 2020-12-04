using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Teams.Data.Migrations
{
    public partial class AddedTestRunEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Testrun",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TestedUserId = table.Column<string>(nullable: true),
                    TestId = table.Column<Guid>(nullable: false),
                    InProgress = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Testrun", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuestionAnswerPairs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    QuestionId = table.Column<Guid>(nullable: false),
                    AnswerId = table.Column<Guid>(nullable: false),
                    TestRunID_FK = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionAnswerPairs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionAnswerPairs_Testrun_TestRunID_FK",
                        column: x => x.TestRunID_FK,
                        principalTable: "Testrun",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAnswerPairs_TestRunID_FK",
                table: "QuestionAnswerPairs",
                column: "TestRunID_FK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "MultipleAnswerQuestionOption");

            migrationBuilder.DropTable(
                name: "QuestionAnswerPairs");

            migrationBuilder.DropTable(
                name: "Testrun");

            migrationBuilder.DropColumn(
                name: "Answer",
                table: "Questions");
        }
    }
}
