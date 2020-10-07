using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Teams.Data.Migrations
{
    public partial class SingleQuestionOptionTableNameChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionOption");

            migrationBuilder.CreateTable(
                name: "SingleSelectionQuestionOption",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    IsAnswer = table.Column<bool>(nullable: false),
                    SingleSelectionQuestionId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SingleSelectionQuestionOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SingleSelectionQuestionOption_Questions_SingleSelectionQuestionId",
                        column: x => x.SingleSelectionQuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SingleSelectionQuestionOption_SingleSelectionQuestionId",
                table: "SingleSelectionQuestionOption",
                column: "SingleSelectionQuestionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SingleSelectionQuestionOption");

            migrationBuilder.CreateTable(
                name: "QuestionOption",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsAnswer = table.Column<bool>(type: "bit", nullable: false),
                    SingleSelectionQuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionOption_Questions_SingleSelectionQuestionId",
                        column: x => x.SingleSelectionQuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionOption_SingleSelectionQuestionId",
                table: "QuestionOption",
                column: "SingleSelectionQuestionId");
        }
    }
}
