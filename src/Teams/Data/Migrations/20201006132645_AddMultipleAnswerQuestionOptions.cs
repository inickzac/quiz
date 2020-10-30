using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Teams.Data.Migrations
{
    public partial class AddMultipleAnswerQuestionOptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MultipleAnswerQuestionOption",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    IsRight = table.Column<bool>(nullable: false),
                    MultipleAnswerQuestionId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultipleAnswerQuestionOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MultipleAnswerQuestionOption_Questions_MultipleAnswerQuestionId",
                        column: x => x.MultipleAnswerQuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MultipleAnswerQuestionOption_MultipleAnswerQuestionId",
                table: "MultipleAnswerQuestionOption",
                column: "MultipleAnswerQuestionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MultipleAnswerQuestionOption");
        }
    }
}
