using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PassTrackingSystem.Migrations
{
    public partial class initial : Migration
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
                name: "DocumentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IssuingAuthorities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssuingAuthorities", x => x.Id);
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
                name: "Visitors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Patronymic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlaceOfWork = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visitors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Patronymic = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Series = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfIssue = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DocumentTypeId = table.Column<int>(type: "int", nullable: false),
                    IssuingAuthorityId = table.Column<int>(type: "int", nullable: false),
                    VisitorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documents_DocumentTypes_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalTable: "DocumentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Documents_IssuingAuthorities_IssuingAuthorityId",
                        column: x => x.IssuingAuthorityId,
                        principalTable: "IssuingAuthorities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Documents_Visitors_VisitorId",
                        column: x => x.VisitorId,
                        principalTable: "Visitors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarPasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ValidWith = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValitUntil = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PurposeOfIssuance = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarPassIssuedId = table.Column<int>(type: "int", nullable: true),
                    VisitorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarPasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarPasses_Employees_CarPassIssuedId",
                        column: x => x.CarPassIssuedId,
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
                    ShootingPermissionIssuedId = table.Column<int>(type: "int", nullable: true),
                    VisitorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShootingPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShootingPermissions_Employees_ShootingPermissionIssuedId",
                        column: x => x.ShootingPermissionIssuedId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    SinglePassIssuedId = table.Column<int>(type: "int", nullable: true),
                    VisitorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SinglePasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SinglePasses_Employees_SinglePassIssuedId",
                        column: x => x.SinglePassIssuedId,
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
                name: "TemporaryPasses",
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
                    table.PrimaryKey("PK_TemporaryPasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemporaryPasses_Employees_TemporaryPassIssuedId",
                        column: x => x.TemporaryPassIssuedId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TemporaryPasses_Visitors_VisitorId",
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

            migrationBuilder.CreateTable(
                name: "ShootingPermissionStationFacility",
                columns: table => new
                {
                    ShootingPermissionsId = table.Column<int>(type: "int", nullable: false),
                    StationFacilitiesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShootingPermissionStationFacility", x => new { x.ShootingPermissionsId, x.StationFacilitiesId });
                    table.ForeignKey(
                        name: "FK_ShootingPermissionStationFacility_ShootingPermissions_ShootingPermissionsId",
                        column: x => x.ShootingPermissionsId,
                        principalTable: "ShootingPermissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShootingPermissionStationFacility_StationFacilities_StationFacilitiesId",
                        column: x => x.StationFacilitiesId,
                        principalTable: "StationFacilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SinglePassStationFacility",
                columns: table => new
                {
                    SinglePassesId = table.Column<int>(type: "int", nullable: false),
                    StationFacilitiesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SinglePassStationFacility", x => new { x.SinglePassesId, x.StationFacilitiesId });
                    table.ForeignKey(
                        name: "FK_SinglePassStationFacility_SinglePasses_SinglePassesId",
                        column: x => x.SinglePassesId,
                        principalTable: "SinglePasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SinglePassStationFacility_StationFacilities_StationFacilitiesId",
                        column: x => x.StationFacilitiesId,
                        principalTable: "StationFacilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_CarPasses_CarPassIssuedId",
                table: "CarPasses",
                column: "CarPassIssuedId");

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
                name: "IX_Documents_DocumentTypeId",
                table: "Documents",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_IssuingAuthorityId",
                table: "Documents",
                column: "IssuingAuthorityId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_VisitorId",
                table: "Documents",
                column: "VisitorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ShootingPermissions_ShootingPermissionIssuedId",
                table: "ShootingPermissions",
                column: "ShootingPermissionIssuedId");

            migrationBuilder.CreateIndex(
                name: "IX_ShootingPermissions_VisitorId",
                table: "ShootingPermissions",
                column: "VisitorId");

            migrationBuilder.CreateIndex(
                name: "IX_ShootingPermissionStationFacility_StationFacilitiesId",
                table: "ShootingPermissionStationFacility",
                column: "StationFacilitiesId");

            migrationBuilder.CreateIndex(
                name: "IX_SinglePasses_SinglePassIssuedId",
                table: "SinglePasses",
                column: "SinglePassIssuedId");

            migrationBuilder.CreateIndex(
                name: "IX_SinglePasses_VisitorId",
                table: "SinglePasses",
                column: "VisitorId");

            migrationBuilder.CreateIndex(
                name: "IX_SinglePassStationFacility_StationFacilitiesId",
                table: "SinglePassStationFacility",
                column: "StationFacilitiesId");

            migrationBuilder.CreateIndex(
                name: "IX_StationFacilityTemporaryPass_TemporaryPassesId",
                table: "StationFacilityTemporaryPass",
                column: "TemporaryPassesId");

            migrationBuilder.CreateIndex(
                name: "IX_TemporaryPasses_TemporaryPassIssuedId",
                table: "TemporaryPasses",
                column: "TemporaryPassIssuedId");

            migrationBuilder.CreateIndex(
                name: "IX_TemporaryPasses_VisitorId",
                table: "TemporaryPasses",
                column: "VisitorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "ShootingPermissionStationFacility");

            migrationBuilder.DropTable(
                name: "SinglePassStationFacility");

            migrationBuilder.DropTable(
                name: "StationFacilityTemporaryPass");

            migrationBuilder.DropTable(
                name: "CarPasses");

            migrationBuilder.DropTable(
                name: "DocumentTypes");

            migrationBuilder.DropTable(
                name: "IssuingAuthorities");

            migrationBuilder.DropTable(
                name: "ShootingPermissions");

            migrationBuilder.DropTable(
                name: "SinglePasses");

            migrationBuilder.DropTable(
                name: "StationFacilities");

            migrationBuilder.DropTable(
                name: "TemporaryPasses");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Visitors");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
