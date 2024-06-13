using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class uniqueemail_doctorandpatient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("62f51895-a78b-4cba-bd25-6179df624d68"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3e58e7cf-d619-4e15-8fdd-c41848447259"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "AuthenticatorType", "CreatedDate", "DateOfBirth", "DeletedDate", "Email", "FirstName", "LastName", "NationalIdentity", "PasswordHash", "PasswordSalt", "Phone", "UpdatedDate" },
                values: new object[] { new Guid("2fc0dc45-cf58-4ec3-bdf5-58eed393d7f1"), "Tekirdağ", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2000, 11, 20), null, "fatmabireltr@gmail.com", "Fatma", "Birel", "12345678901", new byte[] { 113, 42, 44, 90, 95, 213, 211, 196, 157, 218, 104, 149, 184, 3, 196, 164, 154, 201, 176, 10, 182, 55, 45, 39, 123, 173, 140, 148, 128, 135, 106, 29, 18, 69, 214, 130, 67, 53, 42, 216, 81, 59, 20, 253, 187, 139, 17, 178, 7, 86, 49, 33, 25, 227, 219, 127, 79, 20, 112, 75, 245, 242, 145, 28 }, new byte[] { 153, 72, 169, 113, 127, 70, 16, 130, 145, 126, 182, 57, 53, 163, 142, 19, 152, 175, 61, 73, 176, 255, 194, 247, 56, 191, 64, 186, 10, 117, 204, 252, 218, 126, 101, 252, 152, 218, 55, 164, 201, 223, 102, 60, 55, 202, 245, 38, 118, 157, 140, 82, 251, 32, 29, 172, 14, 246, 24, 112, 123, 53, 182, 166, 151, 205, 186, 47, 182, 156, 77, 53, 152, 186, 233, 144, 165, 239, 65, 154, 57, 150, 54, 87, 221, 28, 110, 69, 223, 212, 100, 93, 53, 192, 37, 215, 150, 163, 222, 116, 133, 195, 166, 210, 57, 184, 73, 197, 255, 5, 49, 154, 94, 170, 61, 12, 141, 198, 30, 85, 87, 38, 196, 99, 37, 76, 141, 105 }, "05279563492", null });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("df879465-c232-4214-bb19-ea3c17508f04"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null, new Guid("2fc0dc45-cf58-4ec3-bdf5-58eed393d7f1") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                values: new object[] { new Guid("3e58e7cf-d619-4e15-8fdd-c41848447259"), "Tekirdağ", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2000, 11, 20), null, "fatmabireltr@gmail.com", "Fatma", "Birel", "12345678901", new byte[] { 60, 217, 238, 152, 192, 203, 104, 19, 74, 20, 146, 201, 74, 129, 76, 8, 23, 78, 234, 193, 169, 13, 6, 102, 105, 122, 84, 224, 112, 38, 209, 117, 50, 6, 29, 28, 126, 163, 181, 189, 21, 252, 41, 12, 59, 255, 197, 117, 3, 247, 221, 113, 102, 110, 78, 94, 73, 228, 175, 203, 122, 97, 162, 11 }, new byte[] { 193, 41, 53, 136, 134, 68, 177, 122, 92, 20, 181, 193, 142, 161, 144, 41, 207, 126, 146, 212, 127, 16, 253, 135, 219, 237, 10, 180, 159, 26, 63, 10, 249, 176, 84, 187, 77, 79, 42, 72, 96, 54, 8, 164, 38, 119, 16, 208, 76, 186, 196, 103, 159, 130, 254, 214, 0, 22, 105, 219, 235, 182, 150, 17, 238, 188, 25, 164, 179, 207, 33, 154, 196, 11, 113, 47, 159, 186, 95, 37, 47, 208, 231, 185, 34, 156, 62, 203, 150, 117, 198, 203, 156, 66, 244, 208, 64, 26, 8, 184, 22, 185, 117, 226, 85, 243, 25, 38, 105, 153, 253, 11, 206, 49, 158, 207, 55, 23, 130, 246, 94, 179, 17, 207, 6, 100, 20, 132 }, "05279563492", null });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("62f51895-a78b-4cba-bd25-6179df624d68"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null, new Guid("3e58e7cf-d619-4e15-8fdd-c41848447259") });
        }
    }
}
