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
                    BrukerId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
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
                    InnleggId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Dato = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<string>(nullable: true),
                    Tittel = table.Column<string>(nullable: true),
                    Innhold = table.Column<string>(nullable: true),
                    Brukernavn = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Innlegg", x => x.InnleggId);
                });

            migrationBuilder.CreateTable(
                name: "Svar",
                columns: table => new
                {
                    SvarId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Innhold = table.Column<string>(nullable: true),
                    Dato = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<string>(nullable: true),
                    Brukernavn = table.Column<string>(nullable: true),
                    InnleggId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Svar", x => x.SvarId);
                    table.ForeignKey(
                        name: "FK_Svar_Innlegg_InnleggId",
                        column: x => x.InnleggId,
                        principalTable: "Innlegg",
                        principalColumn: "InnleggId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Svar_InnleggId",
                table: "Svar",
                column: "InnleggId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Brukere");

            migrationBuilder.DropTable(
                name: "Svar");

            migrationBuilder.DropTable(
                name: "Innlegg");
        }
    }
}
