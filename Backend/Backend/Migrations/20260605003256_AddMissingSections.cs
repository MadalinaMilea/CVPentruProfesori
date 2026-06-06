using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    public partial class AddMissingSections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Interese",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfesorId = table.Column<int>(type: "int", nullable: false),
                    Nume = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interese", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Interese_Profesori_ProfesorId",
                        column: x => x.ProfesorId,
                        principalTable: "Profesori",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Premii",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfesorId = table.Column<int>(type: "int", nullable: false),
                    Titlu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Acordant = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rezumat = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Premii", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Premii_Profesori_ProfesorId",
                        column: x => x.ProfesorId,
                        principalTable: "Profesori",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Proiecte",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfesorId = table.Column<int>(type: "int", nullable: false),
                    Nume = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataStart = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataEnd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descriere = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proiecte", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Proiecte_Profesori_ProfesorId",
                        column: x => x.ProfesorId,
                        principalTable: "Profesori",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Referinte",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfesorId = table.Column<int>(type: "int", nullable: false),
                    Nume = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Referinta_ = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Referinte", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Referinte_Profesori_ProfesorId",
                        column: x => x.ProfesorId,
                        principalTable: "Profesori",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Voluntariate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfesorId = table.Column<int>(type: "int", nullable: false),
                    Organizatie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pozitie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataStart = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataEnd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rezumat = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voluntariate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Voluntariate_Profesori_ProfesorId",
                        column: x => x.ProfesorId,
                        principalTable: "Profesori",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Interese_ProfesorId",
                table: "Interese",
                column: "ProfesorId");

            migrationBuilder.CreateIndex(
                name: "IX_Premii_ProfesorId",
                table: "Premii",
                column: "ProfesorId");

            migrationBuilder.CreateIndex(
                name: "IX_Proiecte_ProfesorId",
                table: "Proiecte",
                column: "ProfesorId");

            migrationBuilder.CreateIndex(
                name: "IX_Referinte_ProfesorId",
                table: "Referinte",
                column: "ProfesorId");

            migrationBuilder.CreateIndex(
                name: "IX_Voluntariate_ProfesorId",
                table: "Voluntariate",
                column: "ProfesorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Interese");

            migrationBuilder.DropTable(
                name: "Premii");

            migrationBuilder.DropTable(
                name: "Proiecte");

            migrationBuilder.DropTable(
                name: "Referinte");

            migrationBuilder.DropTable(
                name: "Voluntariate");
        }
    }
}
