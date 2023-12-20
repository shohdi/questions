using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace questions.Data.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IS_ANSWERED",
                table: "EXAMS_QUETIONS",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IS_ANSWERED",
                table: "EXAMS_QUETIONS");
        }
    }
}
