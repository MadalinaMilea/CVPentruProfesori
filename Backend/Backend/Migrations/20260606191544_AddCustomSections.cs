using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    public partial class AddCustomSections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SectiuniCustome",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titlu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ordine = table.Column<int>(type: "int", nullable: false),
                    EsteActiva = table.Column<bool>(type: "bit", nullable: false),
                    EsteObligatorie = table.Column<bool>(type: "bit", nullable: false),
                    EsteRepetabila = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectiuniCustome", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CampuriCustome",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SectiuneCustomaId = table.Column<int>(type: "int", nullable: false),
                    Eticheta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ordine = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampuriCustome", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CampuriCustome_SectiuniCustome_SectiuneCustomaId",
                        column: x => x.SectiuneCustomaId,
                        principalTable: "SectiuniCustome",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InregistrariCustome",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SectiuneCustomaId = table.Column<int>(type: "int", nullable: false),
                    ProfesorId = table.Column<int>(type: "int", nullable: false),
                    Ordine = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InregistrariCustome", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InregistrariCustome_Profesori_ProfesorId",
                        column: x => x.ProfesorId,
                        principalTable: "Profesori",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InregistrariCustome_SectiuniCustome_SectiuneCustomaId",
                        column: x => x.SectiuneCustomaId,
                        principalTable: "SectiuniCustome",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ValoriCampuri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InregistrareCustomaId = table.Column<int>(type: "int", nullable: false),
                    CampCustomId = table.Column<int>(type: "int", nullable: false),
                    Valoare = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValoriCampuri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ValoriCampuri_CampuriCustome_CampCustomId",
                        column: x => x.CampCustomId,
                        principalTable: "CampuriCustome",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ValoriCampuri_InregistrariCustome_InregistrareCustomaId",
                        column: x => x.InregistrareCustomaId,
                        principalTable: "InregistrariCustome",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CampuriCustome_SectiuneCustomaId",
                table: "CampuriCustome",
                column: "SectiuneCustomaId");

            migrationBuilder.CreateIndex(
                name: "IX_InregistrariCustome_ProfesorId",
                table: "InregistrariCustome",
                column: "ProfesorId");

            migrationBuilder.CreateIndex(
                name: "IX_InregistrariCustome_SectiuneCustomaId",
                table: "InregistrariCustome",
                column: "SectiuneCustomaId");

            migrationBuilder.CreateIndex(
                name: "IX_ValoriCampuri_CampCustomId",
                table: "ValoriCampuri",
                column: "CampCustomId");

            migrationBuilder.CreateIndex(
                name: "IX_ValoriCampuri_InregistrareCustomaId",
                table: "ValoriCampuri",
                column: "InregistrareCustomaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ValoriCampuri");

            migrationBuilder.DropTable(
                name: "CampuriCustome");

            migrationBuilder.DropTable(
                name: "InregistrariCustome");

            migrationBuilder.DropTable(
                name: "SectiuniCustome");
        }
    }
}
