using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Teams.Domain;

namespace Teams.Data.Migrations
{
    public partial class AddTestRunTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TestRuns",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TestedUserId = table.Column<string>(nullable: true),
                    TestId = table.Column<Guid>(nullable: false),
                    InProgress = table.Column<bool>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestRuns", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AnswerOptions = table.Column<string>(nullable: true),
                    AnswerText = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_TestRuns_Id",
                        column: x => x.Id,
                        principalTable: "TestRuns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "MultipleAnswerQuestionOption");

            migrationBuilder.DropTable(
                name: "TestRuns");

            migrationBuilder.DropColumn(
                name: "Answer",
                table: "Questions");
        }
    }
}
