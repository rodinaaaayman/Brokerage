using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Brokerage.Migrations
{
    /// <inheritdoc />
    public partial class JWT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Commission",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "GrossAmount",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "NetAmount",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "NationalID",
                table: "Clients",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(14)",
                oldMaxLength: 14);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Clients",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "NationalID",
                table: "Clients",
                type: "nvarchar(14)",
                maxLength: 14,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Clients",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Commission",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                computedColumnSql: "([Quantity] * [UnitPrice]) * [CommissionRate] / 100",
                stored: true);

            migrationBuilder.AddColumn<decimal>(
                name: "GrossAmount",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                computedColumnSql: "([Quantity] * [UnitPrice]) + (([Quantity] * [UnitPrice]) * [CommissionRate] / 100)",
                stored: true);

            migrationBuilder.AddColumn<decimal>(
                name: "NetAmount",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                computedColumnSql: "[Quantity] * [UnitPrice]",
                stored: true);
        }
    }
}
