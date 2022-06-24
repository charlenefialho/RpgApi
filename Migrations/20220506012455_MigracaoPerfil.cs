using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RpgApi.Migrations
{
    public partial class MigracaoPerfil : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Perfil",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Jogador");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt", "Username" },
                values: new object[] { new byte[] { 73, 92, 250, 172, 105, 75, 86, 150, 138, 66, 0, 144, 122, 6, 134, 107, 99, 99, 170, 100, 158, 211, 37, 189, 126, 156, 139, 35, 238, 171, 172, 185, 1, 236, 131, 136, 190, 83, 79, 54, 71, 138, 38, 208, 135, 113, 139, 52, 163, 111, 127, 135, 193, 123, 52, 172, 110, 75, 14, 147, 188, 248, 234, 62 }, new byte[] { 181, 250, 94, 182, 192, 93, 159, 10, 158, 12, 91, 177, 253, 193, 221, 130, 53, 143, 245, 105, 56, 41, 33, 230, 136, 73, 246, 117, 111, 58, 26, 205, 174, 1, 150, 191, 178, 234, 153, 235, 4, 41, 235, 85, 131, 80, 216, 166, 169, 122, 238, 68, 233, 67, 30, 220, 37, 221, 150, 127, 247, 1, 226, 95, 254, 6, 115, 73, 212, 52, 142, 93, 134, 37, 48, 66, 195, 237, 4, 184, 185, 118, 14, 165, 111, 138, 16, 151, 199, 211, 194, 217, 192, 74, 129, 238, 43, 202, 246, 142, 227, 125, 215, 63, 116, 111, 105, 121, 255, 175, 86, 186, 201, 14, 250, 93, 223, 189, 55, 221, 168, 159, 130, 67, 112, 247, 103, 108 }, "UsuarioAdmin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Perfil",
                table: "Usuarios");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt", "Username" },
                values: new object[] { new byte[] { 3, 161, 58, 148, 57, 243, 35, 18, 158, 191, 6, 217, 95, 119, 114, 90, 97, 250, 223, 38, 144, 114, 254, 141, 156, 137, 87, 214, 0, 213, 1, 167, 104, 121, 195, 248, 204, 221, 251, 216, 205, 39, 95, 24, 144, 146, 185, 94, 15, 217, 230, 158, 196, 93, 120, 141, 209, 104, 55, 143, 125, 40, 250, 245 }, new byte[] { 185, 98, 127, 23, 226, 32, 71, 144, 119, 172, 187, 166, 139, 225, 213, 196, 72, 143, 17, 45, 12, 43, 127, 185, 40, 211, 105, 185, 91, 145, 145, 229, 64, 174, 48, 5, 18, 159, 92, 48, 110, 93, 170, 35, 67, 16, 120, 114, 228, 112, 145, 151, 54, 191, 194, 124, 23, 176, 116, 183, 243, 28, 27, 4, 52, 27, 121, 240, 246, 227, 251, 24, 95, 116, 192, 180, 201, 86, 34, 202, 94, 225, 10, 82, 31, 58, 77, 43, 223, 8, 41, 150, 53, 124, 50, 112, 149, 109, 170, 2, 244, 42, 246, 8, 17, 155, 187, 11, 91, 122, 163, 62, 129, 204, 162, 128, 22, 167, 205, 57, 131, 180, 113, 237, 104, 136, 56, 139 }, null });
        }
    }
}
