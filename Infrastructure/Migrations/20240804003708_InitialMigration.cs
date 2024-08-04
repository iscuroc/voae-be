using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Faculties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Careers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    FacultyId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Careers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Careers_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Names = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Lastnames = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    EmailConfirmationToken = table.Column<string>(type: "text", nullable: true),
                    EmailConfirmedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EmailConfirmationTokenExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EmailConfirmationSentAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AccountNumber = table.Column<long>(type: "bigint", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    PasswordResetToken = table.Column<string>(type: "text", nullable: true),
                    PasswordResetTokenSentAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PasswordResetTokenExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CareerId = table.Column<int>(type: "integer", nullable: true),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Careers_CareerId",
                        column: x => x.CareerId,
                        principalTable: "Careers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Slug = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(350)", maxLength: 350, nullable: false),
                    Location = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    MainActivities = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Goals = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TotalSpots = table.Column<int>(type: "integer", nullable: false),
                    BannerLink = table.Column<string>(type: "text", nullable: true),
                    LastRequestedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ActivityStatus = table.Column<int>(type: "integer", nullable: false),
                    ReviewedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ReviewObservations = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    SupervisorId = table.Column<int>(type: "integer", nullable: false),
                    CoordinatorId = table.Column<int>(type: "integer", nullable: false),
                    RequestedById = table.Column<int>(type: "integer", nullable: false),
                    ReviewedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Activities_Users_CoordinatorId",
                        column: x => x.CoordinatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Activities_Users_RequestedById",
                        column: x => x.RequestedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Activities_Users_ReviewedById",
                        column: x => x.ReviewedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Activities_Users_SupervisorId",
                        column: x => x.SupervisorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ActivityOrganizer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ActivityId = table.Column<int>(type: "integer", nullable: false),
                    CareerId = table.Column<int>(type: "integer", nullable: true),
                    OrganizationId = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityOrganizer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityOrganizer_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivityOrganizer_Careers_CareerId",
                        column: x => x.CareerId,
                        principalTable: "Careers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActivityOrganizer_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ActivityScopes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Scope = table.Column<int>(type: "integer", nullable: false),
                    Hours = table.Column<int>(type: "integer", nullable: false),
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ForeignActivityCareers",
                columns: table => new
                {
                    ForeingActivitiesId = table.Column<int>(type: "integer", nullable: false),
                    ForeingCareersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForeignActivityCareers", x => new { x.ForeingActivitiesId, x.ForeingCareersId });
                    table.ForeignKey(
                        name: "FK_ForeignActivityCareers_Activities_ForeingActivitiesId",
                        column: x => x.ForeingActivitiesId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ForeignActivityCareers_Careers_ForeingCareersId",
                        column: x => x.ForeingCareersId,
                        principalTable: "Careers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_CoordinatorId",
                table: "Activities",
                column: "CoordinatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_Name",
                table: "Activities",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Activities_RequestedById",
                table: "Activities",
                column: "RequestedById");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_ReviewedById",
                table: "Activities",
                column: "ReviewedById");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_Slug",
                table: "Activities",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Activities_SupervisorId",
                table: "Activities",
                column: "SupervisorId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityOrganizer_ActivityId",
                table: "ActivityOrganizer",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityOrganizer_CareerId",
                table: "ActivityOrganizer",
                column: "CareerId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityOrganizer_OrganizationId",
                table: "ActivityOrganizer",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityScopes_ActivityId",
                table: "ActivityScopes",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Careers_FacultyId",
                table: "Careers",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_ForeignActivityCareers_ForeingCareersId",
                table: "ForeignActivityCareers",
                column: "ForeingCareersId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AccountNumber",
                table: "Users",
                column: "AccountNumber",
                unique: true,
                filter: "\"AccountNumber\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CareerId",
                table: "Users",
                column: "CareerId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityOrganizer");

            migrationBuilder.DropTable(
                name: "ActivityScopes");

            migrationBuilder.DropTable(
                name: "ForeignActivityCareers");

            migrationBuilder.DropTable(
                name: "Organizations");

            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Careers");

            migrationBuilder.DropTable(
                name: "Faculties");
        }
    }
}
