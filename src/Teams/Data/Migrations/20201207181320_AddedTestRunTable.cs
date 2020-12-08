using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Teams.Data.Migrations
{
    public partial class AddedTestRunTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TestRunId",
                table: "Tests",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TestsTakenId",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Testrun",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TestedUserID = table.Column<string>(nullable: true),
                    TestId = table.Column<Guid>(nullable: false),
                    InProgress = table.Column<bool>(nullable: false),
                    AnswersCounter = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Testrun", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Testrun_AspNetUsers_TestedUserID",
                        column: x => x.TestedUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TestRunFK = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_Testrun_TestRunFK",
                        column: x => x.TestRunFK,
                        principalTable: "Testrun",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tests_TestRunId",
                table: "Tests",
                column: "TestRunId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_TestRunFK",
                table: "Answers",
                column: "TestRunFK");

            migrationBuilder.CreateIndex(
                name: "IX_Testrun_TestedUserID",
                table: "Testrun",
                column: "TestedUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_Testrun_TestRunId",
                table: "Tests",
                column: "TestRunId",
                principalTable: "Testrun",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tests_Testrun_TestRunId",
                table: "Tests");

            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "MultipleAnswerQuestionOption");

            migrationBuilder.DropTable(
                name: "Testrun");

            migrationBuilder.DropIndex(
                name: "IX_Tests_TestRunId",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "TestRunId",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "Answer",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "TestsTakenId",
                table: "AspNetUsers");
        }
    }
}
