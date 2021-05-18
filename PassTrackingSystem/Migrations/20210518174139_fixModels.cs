using Microsoft.EntityFrameworkCore.Migrations;

namespace PassTrackingSystem.Migrations
{
    public partial class fixModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarPasses_Employees_TemporaryPassIssuedId",
                table: "CarPasses");

            migrationBuilder.DropForeignKey(
                name: "FK_SinglePasses_Employees_TemporaryPassIssuedId",
                table: "SinglePasses");

            migrationBuilder.RenameColumn(
                name: "TemporaryPassIssuedId",
                table: "SinglePasses",
                newName: "SinglePassIssuedId");

            migrationBuilder.RenameIndex(
                name: "IX_SinglePasses_TemporaryPassIssuedId",
                table: "SinglePasses",
                newName: "IX_SinglePasses_SinglePassIssuedId");

            migrationBuilder.RenameColumn(
                name: "TemporaryPassIssuedId",
                table: "CarPasses",
                newName: "CarPassIssuedId");

            migrationBuilder.RenameIndex(
                name: "IX_CarPasses_TemporaryPassIssuedId",
                table: "CarPasses",
                newName: "IX_CarPasses_CarPassIssuedId");

            migrationBuilder.AddColumn<int>(
                name: "ShootingPermissionIssuedId",
                table: "ShootingPermissions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShootingPermissions_ShootingPermissionIssuedId",
                table: "ShootingPermissions",
                column: "ShootingPermissionIssuedId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarPasses_Employees_CarPassIssuedId",
                table: "CarPasses",
                column: "CarPassIssuedId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShootingPermissions_Employees_ShootingPermissionIssuedId",
                table: "ShootingPermissions",
                column: "ShootingPermissionIssuedId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SinglePasses_Employees_SinglePassIssuedId",
                table: "SinglePasses",
                column: "SinglePassIssuedId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarPasses_Employees_CarPassIssuedId",
                table: "CarPasses");

            migrationBuilder.DropForeignKey(
                name: "FK_ShootingPermissions_Employees_ShootingPermissionIssuedId",
                table: "ShootingPermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_SinglePasses_Employees_SinglePassIssuedId",
                table: "SinglePasses");

            migrationBuilder.DropIndex(
                name: "IX_ShootingPermissions_ShootingPermissionIssuedId",
                table: "ShootingPermissions");

            migrationBuilder.DropColumn(
                name: "ShootingPermissionIssuedId",
                table: "ShootingPermissions");

            migrationBuilder.RenameColumn(
                name: "SinglePassIssuedId",
                table: "SinglePasses",
                newName: "TemporaryPassIssuedId");

            migrationBuilder.RenameIndex(
                name: "IX_SinglePasses_SinglePassIssuedId",
                table: "SinglePasses",
                newName: "IX_SinglePasses_TemporaryPassIssuedId");

            migrationBuilder.RenameColumn(
                name: "CarPassIssuedId",
                table: "CarPasses",
                newName: "TemporaryPassIssuedId");

            migrationBuilder.RenameIndex(
                name: "IX_CarPasses_CarPassIssuedId",
                table: "CarPasses",
                newName: "IX_CarPasses_TemporaryPassIssuedId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarPasses_Employees_TemporaryPassIssuedId",
                table: "CarPasses",
                column: "TemporaryPassIssuedId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SinglePasses_Employees_TemporaryPassIssuedId",
                table: "SinglePasses",
                column: "TemporaryPassIssuedId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
