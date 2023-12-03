using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace questions.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EXAM_REPOSITORIES",
                columns: table => new
                {
                    ID = table.Column<decimal>(type: "decimal(10,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EXAM_REPOSITORIES", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "EXAMS",
                columns: table => new
                {
                    ID = table.Column<decimal>(type: "decimal(10,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    REPO_ID = table.Column<decimal>(type: "decimal(10,0)", nullable: false),
                    ExamRepositoryID = table.Column<decimal>(type: "decimal(10,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EXAMS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EXAMS_EXAM_REPOSITORIES_ExamRepositoryID",
                        column: x => x.ExamRepositoryID,
                        principalTable: "EXAM_REPOSITORIES",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "QUESTIONS",
                columns: table => new
                {
                    ID = table.Column<decimal>(type: "decimal(10,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QUESTION_TEXT = table.Column<string>(type: "nvarchar(2000)", nullable: false),
                    IMAGE_PATH = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    REPO_ID = table.Column<decimal>(type: "decimal(10,0)", nullable: false),
                    ExamRepositoryID = table.Column<decimal>(type: "decimal(10,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QUESTIONS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_QUESTIONS_EXAM_REPOSITORIES_ExamRepositoryID",
                        column: x => x.ExamRepositoryID,
                        principalTable: "EXAM_REPOSITORIES",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "SELECTIONS",
                columns: table => new
                {
                    ID = table.Column<decimal>(type: "decimal(10,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SELECTION_TEXT = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    IS_ANSWER = table.Column<bool>(type: "bit", nullable: false),
                    QUESTION_ID = table.Column<decimal>(type: "decimal(10,0)", nullable: false),
                    QuestionID = table.Column<decimal>(type: "decimal(10,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SELECTIONS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SELECTIONS_QUESTIONS_QuestionID",
                        column: x => x.QuestionID,
                        principalTable: "QUESTIONS",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "EXAMS_QUETIONS",
                columns: table => new
                {
                    ID = table.Column<decimal>(type: "decimal(10,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EXAM_ID = table.Column<decimal>(type: "decimal(10,0)", nullable: false),
                    QUESTION_ID = table.Column<decimal>(type: "decimal(10,0)", nullable: false),
                    ANSWER_ID = table.Column<decimal>(type: "decimal(10,0)", nullable: true),
                    QuestionID = table.Column<decimal>(type: "decimal(10,0)", nullable: true),
                    ExamID = table.Column<decimal>(type: "decimal(10,0)", nullable: true),
                    SelectionID = table.Column<decimal>(type: "decimal(10,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EXAMS_QUETIONS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EXAMS_QUETIONS_EXAMS_ExamID",
                        column: x => x.ExamID,
                        principalTable: "EXAMS",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_EXAMS_QUETIONS_QUESTIONS_QuestionID",
                        column: x => x.QuestionID,
                        principalTable: "QUESTIONS",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_EXAMS_QUETIONS_SELECTIONS_SelectionID",
                        column: x => x.SelectionID,
                        principalTable: "SELECTIONS",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EXAM_REPOSITORIES_NAME",
                table: "EXAM_REPOSITORIES",
                column: "NAME");

            migrationBuilder.CreateIndex(
                name: "IX_EXAMS_ExamRepositoryID",
                table: "EXAMS",
                column: "ExamRepositoryID");

            migrationBuilder.CreateIndex(
                name: "IX_EXAMS_QUETIONS_ExamID",
                table: "EXAMS_QUETIONS",
                column: "ExamID");

            migrationBuilder.CreateIndex(
                name: "IX_EXAMS_QUETIONS_QuestionID",
                table: "EXAMS_QUETIONS",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_EXAMS_QUETIONS_SelectionID",
                table: "EXAMS_QUETIONS",
                column: "SelectionID");

            migrationBuilder.CreateIndex(
                name: "IX_QUESTIONS_ExamRepositoryID",
                table: "QUESTIONS",
                column: "ExamRepositoryID");

            migrationBuilder.CreateIndex(
                name: "IX_QUESTIONS_IMAGE_PATH",
                table: "QUESTIONS",
                column: "IMAGE_PATH");

            migrationBuilder.CreateIndex(
                name: "IX_QUESTIONS_QUESTION_TEXT",
                table: "QUESTIONS",
                column: "QUESTION_TEXT");

            migrationBuilder.CreateIndex(
                name: "IX_SELECTIONS_IS_ANSWER",
                table: "SELECTIONS",
                column: "IS_ANSWER");

            migrationBuilder.CreateIndex(
                name: "IX_SELECTIONS_QuestionID",
                table: "SELECTIONS",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_SELECTIONS_SELECTION_TEXT",
                table: "SELECTIONS",
                column: "SELECTION_TEXT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EXAMS_QUETIONS");

            migrationBuilder.DropTable(
                name: "EXAMS");

            migrationBuilder.DropTable(
                name: "SELECTIONS");

            migrationBuilder.DropTable(
                name: "QUESTIONS");

            migrationBuilder.DropTable(
                name: "EXAM_REPOSITORIES");
        }
    }
}
