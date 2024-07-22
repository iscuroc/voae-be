using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCareerToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CareerId",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Faculties",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<int>(
                name: "FacultyId",
                table: "Careers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_CareerId",
                table: "Users",
                column: "CareerId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_EmailConfirmationToken",
                table: "Users",
                column: "EmailConfirmationToken",
                unique: true,
                filter: "\"EmailConfirmationToken\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PasswordResetToken",
                table: "Users",
                column: "PasswordResetToken",
                unique: true,
                filter: "\"PasswordResetToken\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Careers_FacultyId",
                table: "Careers",
                column: "FacultyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Careers_Faculties_FacultyId",
                table: "Careers",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Careers_CareerId",
                table: "Users",
                column: "CareerId",
                principalTable: "Careers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Careers_Faculties_FacultyId",
                table: "Careers");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Careers_CareerId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CareerId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_EmailConfirmationToken",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_PasswordResetToken",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Careers_FacultyId",
                table: "Careers");

            migrationBuilder.DropColumn(
                name: "CareerId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FacultyId",
                table: "Careers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Faculties",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);
        }
    }
}
