using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace projectStructure.DAL.Migrations
{
    public partial class Somefixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 7, 4, 19, 27, 12, 424, DateTimeKind.Local).AddTicks(871));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 7, 4, 19, 27, 12, 424, DateTimeKind.Local).AddTicks(6870));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2021, 7, 4, 19, 27, 12, 425, DateTimeKind.Local).AddTicks(1443));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2021, 7, 4, 19, 27, 12, 425, DateTimeKind.Local).AddTicks(1475));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2021, 7, 4, 19, 27, 12, 425, DateTimeKind.Local).AddTicks(1486));

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 7, 4, 19, 27, 12, 414, DateTimeKind.Local).AddTicks(3008));

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2021, 7, 4, 19, 27, 12, 423, DateTimeKind.Local).AddTicks(2211));

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2021, 7, 4, 19, 27, 12, 423, DateTimeKind.Local).AddTicks(2302));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegisteredAt",
                value: new DateTime(2021, 7, 4, 19, 27, 12, 423, DateTimeKind.Local).AddTicks(4026));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegisteredAt",
                value: new DateTime(2021, 7, 4, 19, 27, 12, 423, DateTimeKind.Local).AddTicks(8351));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "RegisteredAt",
                value: new DateTime(2021, 7, 4, 19, 27, 12, 423, DateTimeKind.Local).AddTicks(8382));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "RegisteredAt",
                value: new DateTime(2021, 7, 4, 19, 27, 12, 423, DateTimeKind.Local).AddTicks(8390));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 7, 4, 18, 50, 4, 928, DateTimeKind.Local).AddTicks(1685));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 7, 4, 18, 50, 4, 928, DateTimeKind.Local).AddTicks(8486));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2021, 7, 4, 18, 50, 4, 929, DateTimeKind.Local).AddTicks(3732));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2021, 7, 4, 18, 50, 4, 929, DateTimeKind.Local).AddTicks(3772));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2021, 7, 4, 18, 50, 4, 929, DateTimeKind.Local).AddTicks(3781));

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 7, 4, 18, 50, 4, 919, DateTimeKind.Local).AddTicks(9354));

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2021, 7, 4, 18, 50, 4, 926, DateTimeKind.Local).AddTicks(7116));

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2021, 7, 4, 18, 50, 4, 926, DateTimeKind.Local).AddTicks(7204));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegisteredAt",
                value: new DateTime(2021, 7, 4, 18, 50, 4, 926, DateTimeKind.Local).AddTicks(8992));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegisteredAt",
                value: new DateTime(2021, 7, 4, 18, 50, 4, 927, DateTimeKind.Local).AddTicks(5619));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "RegisteredAt",
                value: new DateTime(2021, 7, 4, 18, 50, 4, 927, DateTimeKind.Local).AddTicks(5670));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "RegisteredAt",
                value: new DateTime(2021, 7, 4, 18, 50, 4, 927, DateTimeKind.Local).AddTicks(5681));
        }
    }
}
