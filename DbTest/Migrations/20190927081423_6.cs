using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DbTest.Migrations
{
    public partial class _6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasicTasks_SecurityObjects_SecurityObjectID",
                table: "BasicTasks");

            migrationBuilder.DropIndex(
                name: "IX_BasicTasks_SecurityObjectID",
                table: "BasicTasks");

            migrationBuilder.DropColumn(
                name: "SecurityObjectID",
                table: "BasicTasks");

            migrationBuilder.AddColumn<long>(
                name: "SecurityGroupId",
                table: "BasicTasks",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BasicTaskAcl",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EntityID = table.Column<long>(nullable: false),
                    Permission = table.Column<int>(nullable: false),
                    SecurityObjectID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasicTaskAcl", x => x.ID);
                });

            migrationBuilder.InsertData(
                table: "SecurityObjects",
                columns: new[] { "Id", "Discriminator", "Name" },
                values: new object[] { 2L, "SecurityGroup", "Administrators" });

            migrationBuilder.CreateIndex(
                name: "IX_BasicTasks_SecurityGroupId",
                table: "BasicTasks",
                column: "SecurityGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_BasicTasks_SecurityObjects_SecurityGroupId",
                table: "BasicTasks",
                column: "SecurityGroupId",
                principalTable: "SecurityObjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasicTasks_SecurityObjects_SecurityGroupId",
                table: "BasicTasks");

            migrationBuilder.DropTable(
                name: "BasicTaskAcl");

            migrationBuilder.DropIndex(
                name: "IX_BasicTasks_SecurityGroupId",
                table: "BasicTasks");

            migrationBuilder.DeleteData(
                table: "SecurityObjects",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DropColumn(
                name: "SecurityGroupId",
                table: "BasicTasks");

            migrationBuilder.AddColumn<long>(
                name: "SecurityObjectID",
                table: "BasicTasks",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_BasicTasks_SecurityObjectID",
                table: "BasicTasks",
                column: "SecurityObjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_BasicTasks_SecurityObjects_SecurityObjectID",
                table: "BasicTasks",
                column: "SecurityObjectID",
                principalTable: "SecurityObjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
