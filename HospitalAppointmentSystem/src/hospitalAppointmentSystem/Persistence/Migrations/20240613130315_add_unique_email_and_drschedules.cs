using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class add_unique_email_and_drschedules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DoctorSchedules_DoctorID",
                table: "DoctorSchedules");

            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("de075674-5259-4dd2-b414-9b86f4505e19"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("98d841a2-b19a-4731-ae26-b3fcda255db9"));

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "AuthenticatorType", "CreatedDate", "DateOfBirth", "DeletedDate", "Email", "FirstName", "LastName", "NationalIdentity", "PasswordHash", "PasswordSalt", "Phone", "UpdatedDate" },
                values: new object[] { new Guid("3e58e7cf-d619-4e15-8fdd-c41848447259"), "Tekirdağ", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2000, 11, 20), null, "fatmabireltr@gmail.com", "Fatma", "Birel", "12345678901", new byte[] { 60, 217, 238, 152, 192, 203, 104, 19, 74, 20, 146, 201, 74, 129, 76, 8, 23, 78, 234, 193, 169, 13, 6, 102, 105, 122, 84, 224, 112, 38, 209, 117, 50, 6, 29, 28, 126, 163, 181, 189, 21, 252, 41, 12, 59, 255, 197, 117, 3, 247, 221, 113, 102, 110, 78, 94, 73, 228, 175, 203, 122, 97, 162, 11 }, new byte[] { 193, 41, 53, 136, 134, 68, 177, 122, 92, 20, 181, 193, 142, 161, 144, 41, 207, 126, 146, 212, 127, 16, 253, 135, 219, 237, 10, 180, 159, 26, 63, 10, 249, 176, 84, 187, 77, 79, 42, 72, 96, 54, 8, 164, 38, 119, 16, 208, 76, 186, 196, 103, 159, 130, 254, 214, 0, 22, 105, 219, 235, 182, 150, 17, 238, 188, 25, 164, 179, 207, 33, 154, 196, 11, 113, 47, 159, 186, 95, 37, 47, 208, 231, 185, 34, 156, 62, 203, 150, 117, 198, 203, 156, 66, 244, 208, 64, 26, 8, 184, 22, 185, 117, 226, 85, 243, 25, 38, 105, 153, 253, 11, 206, 49, 158, 207, 55, 23, 130, 246, 94, 179, 17, 207, 6, 100, 20, 132 }, "05279563492", null });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("62f51895-a78b-4cba-bd25-6179df624d68"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null, new Guid("3e58e7cf-d619-4e15-8fdd-c41848447259") });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSchedules_DoctorID_Date",
                table: "DoctorSchedules",
                columns: new[] { "DoctorID", "Date" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_DoctorSchedules_DoctorID_Date",
                table: "DoctorSchedules");

            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("62f51895-a78b-4cba-bd25-6179df624d68"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3e58e7cf-d619-4e15-8fdd-c41848447259"));

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "AuthenticatorType", "CreatedDate", "DateOfBirth", "DeletedDate", "Email", "FirstName", "LastName", "NationalIdentity", "PasswordHash", "PasswordSalt", "Phone", "UpdatedDate" },
                values: new object[] { new Guid("98d841a2-b19a-4731-ae26-b3fcda255db9"), "Tekirdağ", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2000, 11, 20), null, "fatmabireltr@gmail.com", "Fatma", "Birel", "12345678901", new byte[] { 110, 53, 170, 95, 113, 28, 156, 199, 103, 198, 36, 186, 155, 123, 101, 195, 249, 37, 211, 212, 106, 212, 177, 125, 35, 248, 21, 139, 116, 84, 40, 12, 210, 252, 199, 48, 166, 174, 93, 251, 0, 103, 211, 101, 101, 200, 119, 173, 28, 184, 36, 130, 99, 159, 69, 110, 97, 245, 247, 217, 88, 74, 59, 17 }, new byte[] { 114, 116, 243, 223, 95, 14, 52, 246, 74, 101, 126, 138, 242, 220, 10, 134, 195, 168, 0, 120, 13, 3, 6, 154, 229, 30, 183, 22, 228, 37, 83, 88, 218, 234, 9, 18, 8, 84, 83, 239, 88, 36, 183, 146, 117, 53, 196, 173, 118, 175, 134, 50, 229, 48, 32, 93, 80, 156, 56, 115, 226, 11, 192, 8, 96, 184, 221, 110, 234, 125, 85, 21, 68, 153, 251, 136, 122, 99, 71, 33, 215, 218, 163, 22, 154, 88, 5, 253, 183, 178, 95, 154, 152, 66, 236, 207, 100, 42, 102, 42, 89, 133, 141, 191, 124, 103, 37, 76, 167, 144, 59, 94, 218, 15, 30, 101, 154, 216, 165, 159, 63, 189, 177, 142, 189, 12, 205, 65 }, "05279563492", null });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("de075674-5259-4dd2-b414-9b86f4505e19"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null, new Guid("98d841a2-b19a-4731-ae26-b3fcda255db9") });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSchedules_DoctorID",
                table: "DoctorSchedules",
                column: "DoctorID");
        }
    }
}
