using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class adduniquenationalidentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("df879465-c232-4214-bb19-ea3c17508f04"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2fc0dc45-cf58-4ec3-bdf5-58eed393d7f1"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "AuthenticatorType", "CreatedDate", "DateOfBirth", "DeletedDate", "Email", "FirstName", "LastName", "NationalIdentity", "PasswordHash", "PasswordSalt", "Phone", "UpdatedDate" },
                values: new object[] { new Guid("73a4fc56-e8ac-47b5-b43a-c31ca9062219"), "Tekirdağ", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2000, 11, 20), null, "fatmabireltr@gmail.com", "Fatma", "Birel", "12345678901", new byte[] { 201, 255, 144, 237, 121, 195, 64, 175, 20, 4, 237, 76, 48, 174, 231, 203, 95, 154, 132, 17, 187, 121, 209, 161, 247, 78, 191, 156, 90, 226, 40, 200, 249, 151, 192, 138, 27, 168, 122, 236, 225, 238, 59, 28, 236, 228, 13, 92, 168, 144, 83, 236, 18, 102, 11, 249, 200, 152, 124, 222, 134, 228, 194, 70 }, new byte[] { 193, 146, 207, 255, 195, 255, 226, 77, 9, 37, 19, 130, 72, 68, 56, 195, 169, 106, 141, 106, 20, 199, 187, 250, 55, 143, 9, 142, 251, 102, 221, 115, 167, 73, 9, 117, 175, 254, 240, 26, 207, 54, 215, 155, 242, 197, 125, 171, 181, 224, 168, 59, 168, 158, 106, 183, 65, 182, 31, 132, 153, 190, 82, 215, 72, 154, 44, 0, 197, 216, 101, 5, 184, 49, 46, 144, 26, 10, 164, 25, 85, 220, 26, 8, 210, 209, 125, 70, 167, 62, 17, 15, 245, 197, 240, 38, 48, 178, 248, 252, 159, 106, 162, 244, 59, 212, 179, 181, 39, 221, 210, 190, 194, 45, 211, 251, 10, 107, 166, 58, 106, 35, 17, 134, 36, 245, 6, 254 }, "05279563492", null });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("3567e31f-6635-4d58-8b9d-78db3d96d064"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null, new Guid("73a4fc56-e8ac-47b5-b43a-c31ca9062219") });

            migrationBuilder.CreateIndex(
                name: "IX_Users_NationalIdentity",
                table: "Users",
                column: "NationalIdentity",
                unique: true,
                filter: "[NationalIdentity] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_NationalIdentity",
                table: "Users");

            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("3567e31f-6635-4d58-8b9d-78db3d96d064"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("73a4fc56-e8ac-47b5-b43a-c31ca9062219"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "AuthenticatorType", "CreatedDate", "DateOfBirth", "DeletedDate", "Email", "FirstName", "LastName", "NationalIdentity", "PasswordHash", "PasswordSalt", "Phone", "UpdatedDate" },
                values: new object[] { new Guid("2fc0dc45-cf58-4ec3-bdf5-58eed393d7f1"), "Tekirdağ", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2000, 11, 20), null, "fatmabireltr@gmail.com", "Fatma", "Birel", "12345678901", new byte[] { 113, 42, 44, 90, 95, 213, 211, 196, 157, 218, 104, 149, 184, 3, 196, 164, 154, 201, 176, 10, 182, 55, 45, 39, 123, 173, 140, 148, 128, 135, 106, 29, 18, 69, 214, 130, 67, 53, 42, 216, 81, 59, 20, 253, 187, 139, 17, 178, 7, 86, 49, 33, 25, 227, 219, 127, 79, 20, 112, 75, 245, 242, 145, 28 }, new byte[] { 153, 72, 169, 113, 127, 70, 16, 130, 145, 126, 182, 57, 53, 163, 142, 19, 152, 175, 61, 73, 176, 255, 194, 247, 56, 191, 64, 186, 10, 117, 204, 252, 218, 126, 101, 252, 152, 218, 55, 164, 201, 223, 102, 60, 55, 202, 245, 38, 118, 157, 140, 82, 251, 32, 29, 172, 14, 246, 24, 112, 123, 53, 182, 166, 151, 205, 186, 47, 182, 156, 77, 53, 152, 186, 233, 144, 165, 239, 65, 154, 57, 150, 54, 87, 221, 28, 110, 69, 223, 212, 100, 93, 53, 192, 37, 215, 150, 163, 222, 116, 133, 195, 166, 210, 57, 184, 73, 197, 255, 5, 49, 154, 94, 170, 61, 12, 141, 198, 30, 85, 87, 38, 196, 99, 37, 76, 141, 105 }, "05279563492", null });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("df879465-c232-4214-bb19-ea3c17508f04"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null, new Guid("2fc0dc45-cf58-4ec3-bdf5-58eed393d7f1") });
        }
    }
}
