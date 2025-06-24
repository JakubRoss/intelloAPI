using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kontrahenci",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Symbol = table.Column<string>(type: "TEXT", nullable: false),
                    Nazwa = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kontrahenci", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Towar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NazwaTowaru = table.Column<string>(type: "TEXT", nullable: false),
                    JednostkaMiary = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Towar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DokumentyPrzyjecia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Data = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Symbol = table.Column<string>(type: "TEXT", nullable: false),
                    KontrahentId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DokumentyPrzyjecia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DokumentyPrzyjecia_Kontrahenci_KontrahentId",
                        column: x => x.KontrahentId,
                        principalTable: "Kontrahenci",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PozycjeDokumentow",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DokumentPrzyjeciaId = table.Column<int>(type: "INTEGER", nullable: false),
                    TowarId = table.Column<int>(type: "INTEGER", nullable: false),
                    Ilosc = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PozycjeDokumentow", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PozycjeDokumentow_DokumentyPrzyjecia_DokumentPrzyjeciaId",
                        column: x => x.DokumentPrzyjeciaId,
                        principalTable: "DokumentyPrzyjecia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PozycjeDokumentow_Towar_TowarId",
                        column: x => x.TowarId,
                        principalTable: "Towar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DokumentyPrzyjecia_KontrahentId",
                table: "DokumentyPrzyjecia",
                column: "KontrahentId");

            migrationBuilder.CreateIndex(
                name: "IX_PozycjeDokumentow_DokumentPrzyjeciaId",
                table: "PozycjeDokumentow",
                column: "DokumentPrzyjeciaId");

            migrationBuilder.CreateIndex(
                name: "IX_PozycjeDokumentow_TowarId",
                table: "PozycjeDokumentow",
                column: "TowarId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PozycjeDokumentow");

            migrationBuilder.DropTable(
                name: "DokumentyPrzyjecia");

            migrationBuilder.DropTable(
                name: "Towar");

            migrationBuilder.DropTable(
                name: "Kontrahenci");
        }
    }
}
