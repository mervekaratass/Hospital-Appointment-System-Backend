using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class isApprovedFeedback : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("3567e31f-6635-4d58-8b9d-78db3d96d064"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("73a4fc56-e8ac-47b5-b43a-c31ca9062219"));

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Feedbacks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "AuthenticatorType", "CreatedDate", "DateOfBirth", "DeletedDate", "Email", "FirstName", "LastName", "NationalIdentity", "PasswordHash", "PasswordSalt", "Phone", "UpdatedDate" },
                values: new object[] { new Guid("5f46f4a4-fa53-4437-8e90-b1282ffd1174"), "Tekirdağ", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2000, 11, 20), null, "fatmabireltr@gmail.com", "Fatma", "Birel", "12345678901", new byte[] { 71, 213, 175, 60, 231, 185, 236, 213, 124, 4, 112, 228, 74, 13, 165, 80, 206, 23, 255, 58, 242, 194, 19, 167, 79, 11, 208, 149, 114, 71, 146, 39, 215, 131, 27, 22, 44, 140, 96, 128, 24, 0, 61, 190, 120, 118, 198, 172, 78, 165, 169, 112, 69, 210, 94, 45, 147, 127, 62, 171, 2, 64, 226, 44 }, new byte[] { 78, 28, 191, 102, 55, 21, 231, 180, 45, 203, 116, 83, 93, 35, 159, 129, 49, 153, 35, 80, 110, 199, 70, 157, 235, 205, 50, 179, 224, 68, 38, 212, 171, 99, 125, 44, 52, 44, 122, 31, 161, 83, 247, 226, 16, 133, 220, 168, 18, 78, 117, 213, 255, 2, 168, 162, 109, 92, 67, 163, 178, 72, 77, 184, 22, 188, 56, 212, 250, 119, 116, 96, 215, 203, 142, 117, 158, 95, 51, 42, 145, 168, 77, 79, 119, 193, 98, 249, 250, 57, 140, 226, 48, 224, 76, 56, 82, 106, 211, 152, 160, 181, 119, 209, 0, 24, 92, 251, 198, 31, 244, 1, 71, 134, 213, 137, 120, 39, 131, 4, 124, 198, 148, 37, 110, 241, 178, 181 }, "05279563492", null });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("33b36a39-c78f-41c2-977c-404867a8a1ef"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null, new Guid("5f46f4a4-fa53-4437-8e90-b1282ffd1174") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("33b36a39-c78f-41c2-977c-404867a8a1ef"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5f46f4a4-fa53-4437-8e90-b1282ffd1174"));

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Feedbacks");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "AuthenticatorType", "CreatedDate", "DateOfBirth", "DeletedDate", "Email", "FirstName", "LastName", "NationalIdentity", "PasswordHash", "PasswordSalt", "Phone", "UpdatedDate" },
                values: new object[] { new Guid("73a4fc56-e8ac-47b5-b43a-c31ca9062219"), "Tekirdağ", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2000, 11, 20), null, "fatmabireltr@gmail.com", "Fatma", "Birel", "12345678901", new byte[] { 201, 255, 144, 237, 121, 195, 64, 175, 20, 4, 237, 76, 48, 174, 231, 203, 95, 154, 132, 17, 187, 121, 209, 161, 247, 78, 191, 156, 90, 226, 40, 200, 249, 151, 192, 138, 27, 168, 122, 236, 225, 238, 59, 28, 236, 228, 13, 92, 168, 144, 83, 236, 18, 102, 11, 249, 200, 152, 124, 222, 134, 228, 194, 70 }, new byte[] { 193, 146, 207, 255, 195, 255, 226, 77, 9, 37, 19, 130, 72, 68, 56, 195, 169, 106, 141, 106, 20, 199, 187, 250, 55, 143, 9, 142, 251, 102, 221, 115, 167, 73, 9, 117, 175, 254, 240, 26, 207, 54, 215, 155, 242, 197, 125, 171, 181, 224, 168, 59, 168, 158, 106, 183, 65, 182, 31, 132, 153, 190, 82, 215, 72, 154, 44, 0, 197, 216, 101, 5, 184, 49, 46, 144, 26, 10, 164, 25, 85, 220, 26, 8, 210, 209, 125, 70, 167, 62, 17, 15, 245, 197, 240, 38, 48, 178, 248, 252, 159, 106, 162, 244, 59, 212, 179, 181, 39, 221, 210, 190, 194, 45, 211, 251, 10, 107, 166, 58, 106, 35, 17, 134, 36, 245, 6, 254 }, "05279563492", null });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("3567e31f-6635-4d58-8b9d-78db3d96d064"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null, new Guid("73a4fc56-e8ac-47b5-b43a-c31ca9062219") });
        }
    }
}
