using Microsoft.EntityFrameworkCore.Migrations;

namespace KlimaOppgave.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Innlegg",
                columns: table => new
                {
                    InnleggId = table.Column<string>(nullable: false),
                    Dato = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<string>(nullable: true),
                    Tittel = table.Column<string>(nullable: true),
                    Innhold = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Innlegg", x => x.InnleggId);
                });

            migrationBuilder.CreateTable(
                name: "Svar",
                columns: table => new
                {
                    SvarId = table.Column<string>(nullable: false),
                    Innhold = table.Column<string>(nullable: true),
                    Dato = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<string>(nullable: true),
                    InnleggId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Svar", x => x.SvarId);
                    table.ForeignKey(
                        name: "FK_Svar_Innlegg_InnleggId",
                        column: x => x.InnleggId,
                        principalTable: "Innlegg",
                        principalColumn: "InnleggId",
                        onDelete: ReferentialAction.Restrict);
                });

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
        }
    }
}
