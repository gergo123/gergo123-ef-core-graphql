using Microsoft.EntityFrameworkCore.Migrations;

namespace DbTest.Migrations
{
    public partial class _4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasicTasks_SecurityObjects_ApprovedBySecurityObjectID",
                table: "BasicTasks");

            migrationBuilder.RenameColumn(
                name: "ApprovedBySecurityObjectID",
                table: "BasicTasks",
                newName: "ApprovedBySecurityIdentityID");

            migrationBuilder.RenameIndex(
                name: "IX_BasicTasks_ApprovedBySecurityObjectID",
                table: "BasicTasks",
                newName: "IX_BasicTasks_ApprovedBySecurityIdentityID");

            migrationBuilder.AddForeignKey(
                name: "FK_BasicTasks_SecurityObjects_ApprovedBySecurityIdentityID",
                table: "BasicTasks",
                column: "ApprovedBySecurityIdentityID",
                principalTable: "SecurityObjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasicTasks_SecurityObjects_ApprovedBySecurityIdentityID",
                table: "BasicTasks");

            migrationBuilder.RenameColumn(
                name: "ApprovedBySecurityIdentityID",
                table: "BasicTasks",
                newName: "ApprovedBySecurityObjectID");

            migrationBuilder.RenameIndex(
                name: "IX_BasicTasks_ApprovedBySecurityIdentityID",
                table: "BasicTasks",
                newName: "IX_BasicTasks_ApprovedBySecurityObjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_BasicTasks_SecurityObjects_ApprovedBySecurityObjectID",
                table: "BasicTasks",
                column: "ApprovedBySecurityObjectID",
                principalTable: "SecurityObjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
