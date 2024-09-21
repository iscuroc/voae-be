using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreCareersAndOrganizations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Careers",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Licenciatura en Comercio Internacional con Orientación en Agroindustria");

            migrationBuilder.UpdateData(
                table: "Careers",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Desarrollo Local");

            migrationBuilder.UpdateData(
                table: "Careers",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Licenciatura en Administración de Empresas Agropecuarias");

            migrationBuilder.UpdateData(
                table: "Careers",
                keyColumn: "Id",
                keyValue: 8,
                column: "Name",
                value: "Licenciatura en Administración y Generación de Empresas");

            migrationBuilder.InsertData(
                table: "Faculties",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ciencias de la Salud", null },
                    { 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ciencias Jurídicas y Sociales", null },
                    { 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Humanidades y Artes", null },
                    { 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "No Aplica", null }
                });

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Estudiantina");

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "VOAE");

            migrationBuilder.InsertData(
                table: "Organizations",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admisiones", null },
                    { 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Salud VOAE", null },
                    { 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Humanidades, Artes y Deporte", null },
                    { 8, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Área de Matemáticas", null },
                    { 9, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Área de Biología y Química", null },
                    { 10, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Área de Inglés", null }
                });

            migrationBuilder.InsertData(
                table: "Careers",
                columns: new[] { "Id", "CreatedAt", "FacultyId", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 11, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, "Pedagogía", null },
                    { 12, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, "No Aplica", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Careers",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Careers",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Faculties",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Faculties",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Faculties",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Faculties",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.UpdateData(
                table: "Careers",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Licenciatura en Comercio Internacional con Orientación en agroindustria");

            migrationBuilder.UpdateData(
                table: "Careers",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Licenciatura en Desarrollo Local");

            migrationBuilder.UpdateData(
                table: "Careers",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Licenciatura en Administración de Empresas");

            migrationBuilder.UpdateData(
                table: "Careers",
                keyColumn: "Id",
                keyValue: 8,
                column: "Name",
                value: "Licenciatura en ");

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "VOAE");

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Estudiantina");
        }
    }
}
