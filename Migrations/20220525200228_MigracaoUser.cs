using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RpgApi.Migrations
{
    public partial class MigracaoUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OponentId",
                table: "Disputas",
                newName: "OponenteId");

            migrationBuilder.AddColumn<byte[]>(
                name: "Foto",
                table: "Usuarios",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 87, 212, 102, 221, 12, 87, 117, 78, 194, 8, 72, 147, 104, 13, 45, 191, 16, 168, 62, 179, 29, 108, 171, 44, 180, 164, 35, 147, 26, 222, 162, 149, 49, 147, 124, 105, 143, 16, 135, 174, 46, 49, 35, 237, 3, 16, 179, 174, 234, 218, 40, 64, 198, 253, 85, 73, 123, 74, 84, 84, 219, 106, 127, 195 }, new byte[] { 31, 195, 208, 143, 220, 153, 120, 100, 10, 169, 62, 173, 196, 6, 141, 60, 49, 130, 189, 35, 186, 20, 158, 115, 90, 152, 87, 244, 25, 180, 51, 84, 116, 69, 166, 65, 150, 137, 247, 67, 141, 250, 197, 4, 29, 51, 184, 219, 212, 139, 110, 190, 15, 188, 174, 161, 232, 220, 148, 200, 194, 46, 29, 142, 152, 227, 96, 19, 16, 63, 161, 206, 254, 212, 163, 65, 119, 202, 121, 42, 126, 224, 248, 10, 55, 179, 72, 16, 135, 223, 182, 188, 17, 87, 128, 113, 40, 87, 117, 60, 56, 105, 175, 229, 177, 154, 31, 37, 214, 164, 238, 89, 132, 118, 39, 64, 220, 86, 55, 210, 140, 248, 75, 243, 68, 4, 171, 203 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Foto",
                table: "Usuarios");

            migrationBuilder.RenameColumn(
                name: "OponenteId",
                table: "Disputas",
                newName: "OponentId");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 6, 208, 17, 242, 67, 169, 173, 64, 113, 93, 39, 120, 244, 23, 61, 51, 6, 10, 133, 174, 112, 181, 160, 26, 131, 129, 254, 201, 113, 31, 105, 185, 36, 31, 36, 175, 26, 202, 53, 63, 17, 71, 128, 104, 7, 1, 17, 5, 24, 52, 76, 129, 161, 80, 63, 6, 92, 200, 102, 220, 24, 116, 253, 33 }, new byte[] { 60, 109, 171, 242, 213, 110, 247, 245, 160, 134, 249, 191, 253, 251, 5, 87, 38, 61, 76, 129, 81, 234, 188, 93, 1, 14, 36, 147, 171, 175, 171, 156, 21, 109, 153, 116, 166, 95, 251, 142, 226, 23, 80, 110, 202, 218, 249, 169, 219, 1, 192, 22, 124, 83, 119, 23, 87, 50, 180, 173, 192, 160, 128, 207, 223, 238, 30, 142, 75, 247, 61, 124, 24, 128, 220, 96, 5, 216, 205, 12, 68, 47, 52, 209, 57, 235, 19, 233, 63, 221, 104, 148, 5, 255, 9, 63, 77, 119, 251, 43, 234, 144, 226, 177, 8, 166, 64, 13, 35, 133, 139, 31, 89, 65, 216, 17, 24, 44, 112, 157, 148, 71, 237, 65, 238, 209, 33, 86 } });
        }
    }
}
