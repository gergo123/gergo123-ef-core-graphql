using Microsoft.EntityFrameworkCore.Migrations;

namespace DbTest.Migrations
{
    public partial class _5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasicTasks_SecurityObjects_ApprovedBySecurityIdentityID",
                table: "BasicTasks");

            migrationBuilder.AlterColumn<long>(
                name: "ApprovedBySecurityIdentityID",
                table: "BasicTasks",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_BasicTasks_SecurityObjects_ApprovedBySecurityIdentityID",
                table: "BasicTasks",
                column: "ApprovedBySecurityIdentityID",
                principalTable: "SecurityObjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasicTasks_SecurityObjects_ApprovedBySecurityIdentityID",
                table: "BasicTasks");

            migrationBuilder.AlterColumn<long>(
                name: "ApprovedBySecurityIdentityID",
                table: "BasicTasks",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BasicTasks_SecurityObjects_ApprovedBySecurityIdentityID",
                table: "BasicTasks",
                column: "ApprovedBySecurityIdentityID",
                principalTable: "SecurityObjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
