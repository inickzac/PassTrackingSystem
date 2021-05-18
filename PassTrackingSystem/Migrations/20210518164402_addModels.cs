using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PassTrackingSystem.Migrations
{
    public partial class addModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShootingPermissionId",
                table: "StationFacilities",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SinglePassId",
                table: "StationFacilities",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CarPasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ValidWith = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValitUntil = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PurposeOfIssuance = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TemporaryPassIssuedId = table.Column<int>(type: "int", nullable: true),
                    VisitorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarPasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarPasses_Employees_TemporaryPassIssuedId",
                        column: x => x.TemporaryPassIssuedId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CarPasses_Visitors_VisitorId",
                        column: x => x.VisitorId,
                        principalTable: "Visitors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShootingPermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ValidWith = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValitUntil = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShootingPurpose = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CameraType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisitorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShootingPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShootingPermissions_Visitors_VisitorId",
                        column: x => x.VisitorId,
                        principalTable: "Visitors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SinglePasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ValidWith = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValitUntil = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PurposeOfIssuance = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TemporaryPassIssuedId = table.Column<int>(type: "int", nullable: true),
                    VisitorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SinglePasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SinglePasses_Employees_TemporaryPassIssuedId",
                        column: x => x.TemporaryPassIssuedId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SinglePasses_Visitors_VisitorId",
                        column: x => x.VisitorId,
                        principalTable: "Visitors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarLicensePlate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarBrand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarPassId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_CarPasses_CarPassId",
                        column: x => x.CarPassId,
                        principalTable: "CarPasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StationFacilities_ShootingPermissionId",
                table: "StationFacilities",
                column: "ShootingPermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_StationFacilities_SinglePassId",
                table: "StationFacilities",
                column: "SinglePassId");

            migrationBuilder.CreateIndex(
                name: "IX_CarPasses_TemporaryPassIssuedId",
                table: "CarPasses",
                column: "TemporaryPassIssuedId");

            migrationBuilder.CreateIndex(
                name: "IX_CarPasses_VisitorId",
                table: "CarPasses",
                column: "VisitorId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CarPassId",
                table: "Cars",
                column: "CarPassId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShootingPermissions_VisitorId",
                table: "ShootingPermissions",
                column: "VisitorId");

            migrationBuilder.CreateIndex(
                name: "IX_SinglePasses_TemporaryPassIssuedId",
                table: "SinglePasses",
                column: "TemporaryPassIssuedId");

            migrationBuilder.CreateIndex(
                name: "IX_SinglePasses_VisitorId",
                table: "SinglePasses",
                column: "VisitorId");

            migrationBuilder.AddForeignKey(
                name: "FK_StationFacilities_ShootingPermissions_ShootingPermissionId",
                table: "StationFacilities",
                column: "ShootingPermissionId",
                principalTable: "ShootingPermissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StationFacilities_SinglePasses_SinglePassId",
                table: "StationFacilities",
                column: "SinglePassId",
                principalTable: "SinglePasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StationFacilities_ShootingPermissions_ShootingPermissionId",
                table: "StationFacilities");

            migrationBuilder.DropForeignKey(
                name: "FK_StationFacilities_SinglePasses_SinglePassId",
                table: "StationFacilities");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "ShootingPermissions");

            migrationBuilder.DropTable(
                name: "SinglePasses");

            migrationBuilder.DropTable(
                name: "CarPasses");

            migrationBuilder.DropIndex(
                name: "IX_StationFacilities_ShootingPermissionId",
                table: "StationFacilities");

            migrationBuilder.DropIndex(
                name: "IX_StationFacilities_SinglePassId",
                table: "StationFacilities");

            migrationBuilder.DropColumn(
                name: "ShootingPermissionId",
                table: "StationFacilities");

            migrationBuilder.DropColumn(
                name: "SinglePassId",
                table: "StationFacilities");
        }
    }
}
