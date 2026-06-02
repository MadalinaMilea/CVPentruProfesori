using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    public partial class AddCvSections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Certificari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfesorId = table.Column<int>(type: "int", nullable: false),
                    Nume = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Emitent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ordine = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificari", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Competente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfesorId = table.Column<int>(type: "int", nullable: false),
                    Nume = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nivel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Keywords = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ordine = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Educatii",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfesorId = table.Column<int>(type: "int", nullable: false),
                    Institutie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Domeniu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipStudii = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataStart = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataEnd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Scor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ordine = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Educatii", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Experiente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfesorId = table.Column<int>(type: "int", nullable: false),
                    Nume = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pozitie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataStart = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataEnd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descriere = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ordine = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Limbi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfesorId = table.Column<int>(type: "int", nullable: false),
                    Nume = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fluenta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ordine = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Limbi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Linkuri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfesorId = table.Column<int>(type: "int", nullable: false),
                    Retea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ordine = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Linkuri", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Publicatii",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfesorId = table.Column<int>(type: "int", nullable: false),
                    Nume = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Editor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataPublicare = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rezumat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ordine = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publicatii", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sectiuni",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titlu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EsteActiva = table.Column<bool>(type: "bit", nullable: false),
                    EsteObligatorie = table.Column<bool>(type: "bit", nullable: false),
                    Ordine = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sectiuni", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Certificari");

            migrationBuilder.DropTable(
                name: "Competente");

            migrationBuilder.DropTable(
                name: "Educatii");

            migrationBuilder.DropTable(
                name: "Experiente");

            migrationBuilder.DropTable(
                name: "Limbi");

            migrationBuilder.DropTable(
                name: "Linkuri");

            migrationBuilder.DropTable(
                name: "Publicatii");

            migrationBuilder.DropTable(
                name: "Sectiuni");
        }
    }
}
