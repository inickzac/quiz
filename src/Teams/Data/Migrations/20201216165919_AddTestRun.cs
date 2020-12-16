using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Teams.Data.Migrations
{
    public partial class AddTestRun : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<string>(
            //    name: "Answer",
            //    table: "Questions",
            //    nullable: true);

            migrationBuilder.CreateTable(
                name: "AnswerValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AnswerText = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerValues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Testrun",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    InProgress = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Testrun", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AnswerValueId = table.Column<Guid>(nullable: true),
                    TestRunId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_AnswerValues_AnswerValueId",
                        column: x => x.AnswerValueId,
                        principalTable: "AnswerValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Answers_Testrun_TestRunId",
                        column: x => x.TestRunId,
                        principalTable: "Testrun",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_AnswerValueId",
                table: "Answers",
                column: "AnswerValueId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_TestRunId",
                table: "Answers",
                column: "TestRunId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "MultipleAnswerQuestionOption");

            migrationBuilder.DropTable(
                name: "AnswerValues");

            migrationBuilder.DropTable(
                name: "Testrun");

            migrationBuilder.DropColumn(
                name: "Answer",
                table: "Questions");
        }
    }
}
