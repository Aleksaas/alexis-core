using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AlexisCorePro.Domain.Migrations
{
    public partial class InitialModelsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Company_CompanyId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Ships_Customer_CustomerId",
                table: "Ships");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customer",
                table: "Customer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Company",
                table: "Company");

            migrationBuilder.RenameTable(
                name: "Customer",
                newName: "Customers");

            migrationBuilder.RenameTable(
                name: "Company",
                newName: "Companies");

            migrationBuilder.RenameIndex(
                name: "IX_Customer_CompanyId",
                table: "Customers",
                newName: "IX_Customers_CompanyId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Ships",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<byte[]>(
                name: "Timestamp",
                table: "Ships",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "Ships",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Customers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<byte[]>(
                name: "Timestamp",
                table: "Customers",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "Customers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Companies",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<byte[]>(
                name: "Timestamp",
                table: "Companies",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "Companies",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Companies",
                table: "Companies",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Equipment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdateAt = table.Column<DateTime>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Position = table.Column<string>(nullable: true),
                    SerialNumber = table.Column<string>(nullable: true),
                    RunningHours = table.Column<int>(nullable: false),
                    Criticality = table.Column<int>(nullable: false),
                    ShipId = table.Column<int>(nullable: false),
                    EquipmentTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Equipment_Ships_ShipId",
                        column: x => x.ShipId,
                        principalTable: "Ships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Postnumbers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Postnr = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    CountyNumber = table.Column<string>(nullable: true),
                    County = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Postnumbers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_ShipId",
                table: "Equipment",
                column: "ShipId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Companies_CompanyId",
                table: "Customers",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ships_Customers_CustomerId",
                table: "Ships",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Companies_CompanyId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Ships_Customers_CustomerId",
                table: "Ships");

            migrationBuilder.DropTable(
                name: "Equipment");

            migrationBuilder.DropTable(
                name: "Postnumbers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Companies",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Ships");

            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "Ships");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "Ships");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "Companies");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "Customer");

            migrationBuilder.RenameTable(
                name: "Companies",
                newName: "Company");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_CompanyId",
                table: "Customer",
                newName: "IX_Customer_CompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customer",
                table: "Customer",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Company",
                table: "Company",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Company_CompanyId",
                table: "Customer",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ships_Customer_CustomerId",
                table: "Ships",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
