using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KlimaOppgave.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brukere",
                columns: table => new
                {
                    BrukerId = table.Column<string>(nullable: false),
                    Brukernavn = table.Column<string>(nullable: true),
                    Passord = table.Column<byte[]>(nullable: true),
                    Salt = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brukere", x => x.BrukerId);
                });

            migrationBuilder.CreateTable(
                name: "Innlegg",
                columns: table => new
                {
                    InnleggId = table.Column<string>(nullable: false),
                    Dato = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<string>(nullable: true),
                    Tittel = table.Column<string>(nullable: true),
                    Innhold = table.Column<string>(nullable: true),
                    BrukerId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Innlegg", x => x.InnleggId);
                    table.ForeignKey(
                        name: "FK_Innlegg_Brukere_BrukerId",
                        column: x => x.BrukerId,
                        principalTable: "Brukere",
                        principalColumn: "BrukerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Svar",
                columns: table => new
                {
                    SvarId = table.Column<string>(nullable: false),
                    BrukerId = table.Column<string>(nullable: true),
                    Innhold = table.Column<string>(nullable: true),
                    Dato = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<string>(nullable: true),
                    InnleggId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Svar", x => x.SvarId);
                    table.ForeignKey(
                        name: "FK_Svar_Brukere_BrukerId",
                        column: x => x.BrukerId,
                        principalTable: "Brukere",
                        principalColumn: "BrukerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Svar_Innlegg_InnleggId",
                        column: x => x.InnleggId,
                        principalTable: "Innlegg",
                        principalColumn: "InnleggId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Innlegg_BrukerId",
                table: "Innlegg",
                column: "BrukerId");

            migrationBuilder.CreateIndex(
                name: "IX_Svar_BrukerId",
                table: "Svar",
                column: "BrukerId");

            migrationBuilder.CreateIndex(
                name: "IX_Svar_InnleggId",
                table: "Svar",
                column: "InnleggId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Svar");

            migrationBuilder.DropTable(
                name: "Innlegg");

            migrationBuilder.DropTable(
                name: "Brukere");
        }
    }
}
