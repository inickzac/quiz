using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Teams.Data.Migrations
{
    public partial class AddTestRunTable : Migration
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "MultipleAnswerQuestionOption");

            migrationBuilder.DropTable(
                name: "Testrun");

            migrationBuilder.DropColumn(
                name: "Answer",
                table: "Questions");
        }
    }
}
