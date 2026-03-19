using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TranslationApp_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TranslationLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Translator = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    InputText = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    OutputText = table.Column<string>(type: "text", nullable: true),
                    ProviderStatusCode = table.Column<int>(type: "integer", nullable: true),
                    IsSuccess = table.Column<bool>(type: "boolean", nullable: false),
                    ErrorMessage = table.Column<string>(type: "text", nullable: true),
                    DurationMs = table.Column<int>(type: "integer", nullable: false),
                    CorrelationId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TranslationLogs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TranslationLogs_CreatedAtUtc",
                table: "TranslationLogs",
                column: "CreatedAtUtc");

            migrationBuilder.CreateIndex(
                name: "IX_TranslationLogs_Translator",
                table: "TranslationLogs",
                column: "Translator");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TranslationLogs");
        }
    }
}
