using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace projectStructure.DAL.Migrations
{
    public partial class AddSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "CreatedAt", "Name" },
                values: new object[] { 1, new DateTime(2021, 7, 4, 18, 50, 4, 919, DateTimeKind.Local).AddTicks(9354), "DreamTeam" });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "CreatedAt", "Name" },
                values: new object[] { 2, new DateTime(2021, 7, 4, 18, 50, 4, 926, DateTimeKind.Local).AddTicks(7116), "SuperTeam" });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "CreatedAt", "Name" },
                values: new object[] { 3, new DateTime(2021, 7, 4, 18, 50, 4, 926, DateTimeKind.Local).AddTicks(7204), "Hackers" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BirthDay", "Email", "FirstName", "LastName", "RegisteredAt", "TeamId" },
                values: new object[,]
                {
                    { 1, new DateTime(2002, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "somemail@gmail.com", "Irina", "K", new DateTime(2021, 7, 4, 18, 50, 4, 926, DateTimeKind.Local).AddTicks(8992), 1 },
                    { 2, new DateTime(1975, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "lalala@gmail.com", "Leo", "Di Caprio", new DateTime(2021, 7, 4, 18, 50, 4, 927, DateTimeKind.Local).AddTicks(5619), 1 },
                    { 3, new DateTime(1970, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "tesla@gmail.com", "Ilon", "Musk", new DateTime(2021, 7, 4, 18, 50, 4, 927, DateTimeKind.Local).AddTicks(5670), 2 },
                    { 4, new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "superemail@gmail.com", "Super", "User", new DateTime(2021, 7, 4, 18, 50, 4, 927, DateTimeKind.Local).AddTicks(5681), 2 }
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "AuthorId", "CreatedAt", "Deadline", "Description", "Name", "TeamId" },
                values: new object[] { 1, 1, new DateTime(2021, 7, 4, 18, 50, 4, 928, DateTimeKind.Local).AddTicks(1685), new DateTime(2021, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "unbelievable project", "Super cool project", 1 });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "CreatedAt", "Description", "FinishedAt", "Name", "PerformerId", "ProjectId", "State" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 7, 4, 18, 50, 4, 928, DateTimeKind.Local).AddTicks(8486), "to do something", null, "First task", 1, 1, 0 },
                    { 2, new DateTime(2021, 7, 4, 18, 50, 4, 929, DateTimeKind.Local).AddTicks(3732), "to hack", null, "Secong task", 2, 1, 0 },
                    { 3, new DateTime(2021, 7, 4, 18, 50, 4, 929, DateTimeKind.Local).AddTicks(3772), "to do refactoring", null, "Third task", 1, 1, 0 },
                    { 4, new DateTime(2021, 7, 4, 18, 50, 4, 929, DateTimeKind.Local).AddTicks(3781), "", null, "Final task", 1, 1, 0 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
