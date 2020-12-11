using Microsoft.EntityFrameworkCore.Migrations;

namespace Teams.Data.Migrations
{
    public partial class AddedApplicationUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AnswerText",
                table: "Answers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnswerText",
                table: "Answers");
        }
    }
}
