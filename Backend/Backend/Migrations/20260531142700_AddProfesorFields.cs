using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    public partial class AddProfesorFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Label",
                table: "Profesori",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Summary",
                table: "Profesori",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telefon",
                table: "Profesori",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Profesori",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Publicatii_ProfesorId",
                table: "Publicatii",
                column: "ProfesorId");

            migrationBuilder.CreateIndex(
                name: "IX_Linkuri_ProfesorId",
                table: "Linkuri",
                column: "ProfesorId");

            migrationBuilder.CreateIndex(
                name: "IX_Limbi_ProfesorId",
                table: "Limbi",
                column: "ProfesorId");

            migrationBuilder.CreateIndex(
                name: "IX_Experiente_ProfesorId",
                table: "Experiente",
                column: "ProfesorId");

            migrationBuilder.CreateIndex(
                name: "IX_Educatii_ProfesorId",
                table: "Educatii",
                column: "ProfesorId");

            migrationBuilder.CreateIndex(
                name: "IX_Competente_ProfesorId",
                table: "Competente",
                column: "ProfesorId");

            migrationBuilder.CreateIndex(
                name: "IX_Certificari_ProfesorId",
                table: "Certificari",
                column: "ProfesorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Certificari_Profesori_ProfesorId",
                table: "Certificari",
                column: "ProfesorId",
                principalTable: "Profesori",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Competente_Profesori_ProfesorId",
                table: "Competente",
                column: "ProfesorId",
                principalTable: "Profesori",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Educatii_Profesori_ProfesorId",
                table: "Educatii",
                column: "ProfesorId",
                principalTable: "Profesori",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Experiente_Profesori_ProfesorId",
                table: "Experiente",
                column: "ProfesorId",
                principalTable: "Profesori",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Limbi_Profesori_ProfesorId",
                table: "Limbi",
                column: "ProfesorId",
                principalTable: "Profesori",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Linkuri_Profesori_ProfesorId",
                table: "Linkuri",
                column: "ProfesorId",
                principalTable: "Profesori",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Publicatii_Profesori_ProfesorId",
                table: "Publicatii",
                column: "ProfesorId",
                principalTable: "Profesori",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Certificari_Profesori_ProfesorId",
                table: "Certificari");

            migrationBuilder.DropForeignKey(
                name: "FK_Competente_Profesori_ProfesorId",
                table: "Competente");

            migrationBuilder.DropForeignKey(
                name: "FK_Educatii_Profesori_ProfesorId",
                table: "Educatii");

            migrationBuilder.DropForeignKey(
                name: "FK_Experiente_Profesori_ProfesorId",
                table: "Experiente");

            migrationBuilder.DropForeignKey(
                name: "FK_Limbi_Profesori_ProfesorId",
                table: "Limbi");

            migrationBuilder.DropForeignKey(
                name: "FK_Linkuri_Profesori_ProfesorId",
                table: "Linkuri");

            migrationBuilder.DropForeignKey(
                name: "FK_Publicatii_Profesori_ProfesorId",
                table: "Publicatii");

            migrationBuilder.DropIndex(
                name: "IX_Publicatii_ProfesorId",
                table: "Publicatii");

            migrationBuilder.DropIndex(
                name: "IX_Linkuri_ProfesorId",
                table: "Linkuri");

            migrationBuilder.DropIndex(
                name: "IX_Limbi_ProfesorId",
                table: "Limbi");

            migrationBuilder.DropIndex(
                name: "IX_Experiente_ProfesorId",
                table: "Experiente");

            migrationBuilder.DropIndex(
                name: "IX_Educatii_ProfesorId",
                table: "Educatii");

            migrationBuilder.DropIndex(
                name: "IX_Competente_ProfesorId",
                table: "Competente");

            migrationBuilder.DropIndex(
                name: "IX_Certificari_ProfesorId",
                table: "Certificari");

            migrationBuilder.DropColumn(
                name: "Label",
                table: "Profesori");

            migrationBuilder.DropColumn(
                name: "Summary",
                table: "Profesori");

            migrationBuilder.DropColumn(
                name: "Telefon",
                table: "Profesori");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Profesori");
        }
    }
}
