using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Exam_Online.Migrations
{
    public partial class Move : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exhibits",
                columns: table => new
                {
                    ExhibitID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exhibits", x => x.ExhibitID);
                });

            migrationBuilder.CreateTable(
                name: "QuestionCategories",
                columns: table => new
                {
                    QuestionCategoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Category = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionCategories", x => x.QuestionCategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    AccessLevel = table.Column<int>(nullable: false),
                    EntryDate = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    PassHash = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentID);
                });

            migrationBuilder.CreateTable(
                name: "Tests",
                columns: table => new
                {
                    TestID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    DurationInMinute = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tests", x => x.TestID);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    QuestionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    QuestionCategoryID = table.Column<int>(nullable: false),
                    QuestionType = table.Column<string>(nullable: true),
                    Question1 = table.Column<string>(nullable: true),
                    ExhibitID = table.Column<int>(nullable: false),
                    Points = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.QuestionID);
                    table.ForeignKey(
                        name: "FK_Questions_Exhibits_ExhibitID",
                        column: x => x.ExhibitID,
                        principalTable: "Exhibits",
                        principalColumn: "ExhibitID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Questions_QuestionCategories_QuestionCategoryID",
                        column: x => x.QuestionCategoryID,
                        principalTable: "QuestionCategories",
                        principalColumn: "QuestionCategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Registrations",
                columns: table => new
                {
                    RegistrationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StudentID = table.Column<int>(nullable: false),
                    TestID = table.Column<int>(nullable: false),
                    RegistrationDate = table.Column<DateTime>(nullable: false),
                    Token = table.Column<Guid>(nullable: false),
                    TokenExpireTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registrations", x => x.RegistrationID);
                    table.ForeignKey(
                        name: "FK_Registrations_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Registrations_Tests_TestID",
                        column: x => x.TestID,
                        principalTable: "Tests",
                        principalColumn: "TestID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Choices",
                columns: table => new
                {
                    ChoiceID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Label = table.Column<string>(nullable: true),
                    Points = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    QuestionID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Choices", x => x.ChoiceID);
                    table.ForeignKey(
                        name: "FK_Choices_Questions_QuestionID",
                        column: x => x.QuestionID,
                        principalTable: "Questions",
                        principalColumn: "QuestionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestQuestions",
                columns: table => new
                {
                    TestQuestionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TestID = table.Column<int>(nullable: false),
                    QuestionID = table.Column<int>(nullable: false),
                    QuestionNumber = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestQuestions", x => x.TestQuestionID);
                    table.ForeignKey(
                        name: "FK_TestQuestions_Questions_QuestionID",
                        column: x => x.QuestionID,
                        principalTable: "Questions",
                        principalColumn: "QuestionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestQuestions_Tests_TestID",
                        column: x => x.TestID,
                        principalTable: "Tests",
                        principalColumn: "TestID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionDurations",
                columns: table => new
                {
                    QuestionDurationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RequestTime = table.Column<int>(nullable: false),
                    LeaveTime = table.Column<int>(nullable: false),
                    AnsweredTime = table.Column<int>(nullable: false),
                    RegistrationID = table.Column<int>(nullable: false),
                    TestQuestionID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionDurations", x => x.QuestionDurationID);
                    table.ForeignKey(
                        name: "FK_QuestionDurations_Registrations_RegistrationID",
                        column: x => x.RegistrationID,
                        principalTable: "Registrations",
                        principalColumn: "RegistrationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionDurations_TestQuestions_TestQuestionID",
                        column: x => x.TestQuestionID,
                        principalTable: "TestQuestions",
                        principalColumn: "TestQuestionID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "TestPapers",
                columns: table => new
                {
                    TestPaperID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ChoiceID = table.Column<int>(nullable: false),
                    RegistrationID = table.Column<int>(nullable: false),
                    TestQuestionID = table.Column<int>(nullable: false),
                    Answer = table.Column<string>(nullable: true),
                    MarkScored = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestPapers", x => x.TestPaperID);
                    table.ForeignKey(
                        name: "FK_TestPapers_Choices_ChoiceID",
                        column: x => x.ChoiceID,
                        principalTable: "Choices",
                        principalColumn: "ChoiceID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestPapers_Registrations_RegistrationID",
                        column: x => x.RegistrationID,
                        principalTable: "Registrations",
                        principalColumn: "RegistrationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestPapers_TestQuestions_TestQuestionID",
                        column: x => x.TestQuestionID,
                        principalTable: "TestQuestions",
                        principalColumn: "TestQuestionID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Choices_QuestionID",
                table: "Choices",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionDurations_RegistrationID",
                table: "QuestionDurations",
                column: "RegistrationID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionDurations_TestQuestionID",
                table: "QuestionDurations",
                column: "TestQuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_ExhibitID",
                table: "Questions",
                column: "ExhibitID");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_QuestionCategoryID",
                table: "Questions",
                column: "QuestionCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_StudentID",
                table: "Registrations",
                column: "StudentID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_TestID",
                table: "Registrations",
                column: "TestID");

            migrationBuilder.CreateIndex(
                name: "IX_TestPapers_ChoiceID",
                table: "TestPapers",
                column: "ChoiceID");

            migrationBuilder.CreateIndex(
                name: "IX_TestPapers_RegistrationID",
                table: "TestPapers",
                column: "RegistrationID");

            migrationBuilder.CreateIndex(
                name: "IX_TestPapers_TestQuestionID",
                table: "TestPapers",
                column: "TestQuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_TestQuestions_QuestionID",
                table: "TestQuestions",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_TestQuestions_TestID",
                table: "TestQuestions",
                column: "TestID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionDurations");

            migrationBuilder.DropTable(
                name: "TestPapers");

            migrationBuilder.DropTable(
                name: "Choices");

            migrationBuilder.DropTable(
                name: "Registrations");

            migrationBuilder.DropTable(
                name: "TestQuestions");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Tests");

            migrationBuilder.DropTable(
                name: "Exhibits");

            migrationBuilder.DropTable(
                name: "QuestionCategories");
        }
    }
}
