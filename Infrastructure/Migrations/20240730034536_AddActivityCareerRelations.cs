using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddActivityCareerRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityHours");

            migrationBuilder.RenameColumn(
                name: "RequestDate",
                table: "Activities",
                newName: "RequestedAt");

            migrationBuilder.AddColumn<int>(
                name: "MainCareerId",
                table: "Activities",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Activities",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "Activities",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ActivityCareers",
                columns: table => new
                {
                    ForaingActivitiesId = table.Column<int>(type: "integer", nullable: false),
                    ForeingCareersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityCareers", x => new { x.ForaingActivitiesId, x.ForeingCareersId });
                    table.ForeignKey(
                        name: "FK_ActivityCareers_Activities_ForaingActivitiesId",
                        column: x => x.ForaingActivitiesId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivityCareers_Careers_ForeingCareersId",
                        column: x => x.ForeingCareersId,
                        principalTable: "Careers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActivityScopes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Scope = table.Column<int>(type: "integer", nullable: false),
                    HourAmount = table.Column<int>(type: "integer", nullable: false),
                    ActivityId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityScopes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityScopes_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_MainCareerId",
                table: "Activities",
                column: "MainCareerId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_StudentId",
                table: "Activities",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_TeacherId",
                table: "Activities",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityCareers_ForeingCareersId",
                table: "ActivityCareers",
                column: "ForeingCareersId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityScopes_ActivityId",
                table: "ActivityScopes",
                column: "ActivityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Careers_MainCareerId",
                table: "Activities",
                column: "MainCareerId",
                principalTable: "Careers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Users_StudentId",
                table: "Activities",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Users_TeacherId",
                table: "Activities",
                column: "TeacherId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Careers_MainCareerId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Users_StudentId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Users_TeacherId",
                table: "Activities");

            migrationBuilder.DropTable(
                name: "ActivityCareers");

            migrationBuilder.DropTable(
                name: "ActivityScopes");

            migrationBuilder.DropIndex(
                name: "IX_Activities_MainCareerId",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_StudentId",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_TeacherId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "MainCareerId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Activities");

            migrationBuilder.RenameColumn(
                name: "RequestedAt",
                table: "Activities",
                newName: "RequestDate");

            migrationBuilder.CreateTable(
                name: "ActivityHours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ActivityScope = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    HourAmount = table.Column<int>(type: "integer", nullable: false),
                    ParticipationType = table.Column<int>(type: "integer", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityHours", x => x.Id);
                });
        }
    }
}
