using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace questions.Data.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IMAGE_PATH",
                table: "QUESTIONS",
                type: "nvarchar(1000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IMAGE_PATH",
                table: "QUESTIONS",
                type: "nvarchar(1000)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldNullable: true);
        }
    }
}
