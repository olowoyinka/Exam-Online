using Microsoft.EntityFrameworkCore.Migrations;

namespace Exam_Online.Migrations
{
    public partial class low : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "QuestionNumber",
                table: "TestQuestions",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Points",
                table: "Questions",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "QuestionNumber",
                table: "TestQuestions",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Points",
                table: "Questions",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
