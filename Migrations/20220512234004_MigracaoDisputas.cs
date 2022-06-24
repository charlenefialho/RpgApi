using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RpgApi.Migrations
{
    public partial class MigracaoDisputas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Foto",
                table: "Usuarios",
                newName: "FotoPersonagem");

            migrationBuilder.AlterColumn<string>(
                name: "Perfil",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "Jogador",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "Jogador");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Derrotas",
                table: "Personagens",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Disputas",
                table: "Personagens",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Vitorias",
                table: "Personagens",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Disputas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataDisputa = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AtacanteId = table.Column<int>(type: "int", nullable: false),
                    OponentId = table.Column<int>(type: "int", nullable: false),
                    Narracao = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disputas", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 6, 208, 17, 242, 67, 169, 173, 64, 113, 93, 39, 120, 244, 23, 61, 51, 6, 10, 133, 174, 112, 181, 160, 26, 131, 129, 254, 201, 113, 31, 105, 185, 36, 31, 36, 175, 26, 202, 53, 63, 17, 71, 128, 104, 7, 1, 17, 5, 24, 52, 76, 129, 161, 80, 63, 6, 92, 200, 102, 220, 24, 116, 253, 33 }, new byte[] { 60, 109, 171, 242, 213, 110, 247, 245, 160, 134, 249, 191, 253, 251, 5, 87, 38, 61, 76, 129, 81, 234, 188, 93, 1, 14, 36, 147, 171, 175, 171, 156, 21, 109, 153, 116, 166, 95, 251, 142, 226, 23, 80, 110, 202, 218, 249, 169, 219, 1, 192, 22, 124, 83, 119, 23, 87, 50, 180, 173, 192, 160, 128, 207, 223, 238, 30, 142, 75, 247, 61, 124, 24, 128, 220, 96, 5, 216, 205, 12, 68, 47, 52, 209, 57, 235, 19, 233, 63, 221, 104, 148, 5, 255, 9, 63, 77, 119, 251, 43, 234, 144, 226, 177, 8, 166, 64, 13, 35, 133, 139, 31, 89, 65, 216, 17, 24, 44, 112, 157, 148, 71, 237, 65, 238, 209, 33, 86 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Disputas");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Derrotas",
                table: "Personagens");

            migrationBuilder.DropColumn(
                name: "Disputas",
                table: "Personagens");

            migrationBuilder.DropColumn(
                name: "Vitorias",
                table: "Personagens");

            migrationBuilder.RenameColumn(
                name: "FotoPersonagem",
                table: "Usuarios",
                newName: "Foto");

            migrationBuilder.AlterColumn<string>(
                name: "Perfil",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Jogador",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "Jogador");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 73, 92, 250, 172, 105, 75, 86, 150, 138, 66, 0, 144, 122, 6, 134, 107, 99, 99, 170, 100, 158, 211, 37, 189, 126, 156, 139, 35, 238, 171, 172, 185, 1, 236, 131, 136, 190, 83, 79, 54, 71, 138, 38, 208, 135, 113, 139, 52, 163, 111, 127, 135, 193, 123, 52, 172, 110, 75, 14, 147, 188, 248, 234, 62 }, new byte[] { 181, 250, 94, 182, 192, 93, 159, 10, 158, 12, 91, 177, 253, 193, 221, 130, 53, 143, 245, 105, 56, 41, 33, 230, 136, 73, 246, 117, 111, 58, 26, 205, 174, 1, 150, 191, 178, 234, 153, 235, 4, 41, 235, 85, 131, 80, 216, 166, 169, 122, 238, 68, 233, 67, 30, 220, 37, 221, 150, 127, 247, 1, 226, 95, 254, 6, 115, 73, 212, 52, 142, 93, 134, 37, 48, 66, 195, 237, 4, 184, 185, 118, 14, 165, 111, 138, 16, 151, 199, 211, 194, 217, 192, 74, 129, 238, 43, 202, 246, 142, 227, 125, 215, 63, 116, 111, 105, 121, 255, 175, 86, 186, 201, 14, 250, 93, 223, 189, 55, 221, 168, 159, 130, 67, 112, 247, 103, 108 } });
        }
    }
}
