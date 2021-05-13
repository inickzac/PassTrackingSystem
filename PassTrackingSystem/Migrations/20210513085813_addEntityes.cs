using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PassTrackingSystem.Migrations
{
    public partial class addEntityes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StationFacilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StationFacilities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Patronymic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TemporaryPasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ValidWith = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValitUntil = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PurposeOfIssuance = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TemporaryPassIssuedId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemporaryPasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemporaryPasses_Employees_TemporaryPassIssuedId",
                        column: x => x.TemporaryPassIssuedId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StationFacilityTemporaryPass",
                columns: table => new
                {
                    StationFacilitiesId = table.Column<int>(type: "int", nullable: false),
                    TemporaryPassesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StationFacilityTemporaryPass", x => new { x.StationFacilitiesId, x.TemporaryPassesId });
                    table.ForeignKey(
                        name: "FK_StationFacilityTemporaryPass_StationFacilities_StationFacilitiesId",
                        column: x => x.StationFacilitiesId,
                        principalTable: "StationFacilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StationFacilityTemporaryPass_TemporaryPasses_TemporaryPassesId",
                        column: x => x.TemporaryPassesId,
                        principalTable: "TemporaryPasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_StationFacilityTemporaryPass_TemporaryPassesId",
                table: "StationFacilityTemporaryPass",
                column: "TemporaryPassesId");

            migrationBuilder.CreateIndex(
                name: "IX_TemporaryPasses_TemporaryPassIssuedId",
                table: "TemporaryPasses",
                column: "TemporaryPassIssuedId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StationFacilityTemporaryPass");

            migrationBuilder.DropTable(
                name: "StationFacilities");

            migrationBuilder.DropTable(
                name: "TemporaryPasses");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
