using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DbTest.Migrations
{
    public partial class _3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BasicTasks",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EntityId = table.Column<long>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    UniqueID = table.Column<string>(nullable: true),
                    SecurityObjectID = table.Column<long>(nullable: false),
                    ApprovedAt = table.Column<DateTime>(nullable: false),
                    ApprovedBySecurityObjectID = table.Column<long>(nullable: false),
                    UserCreated = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    UserModified = table.Column<string>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: true),
                    Message = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasicTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BasicTasks_SecurityObjects_ApprovedBySecurityObjectID",
                        column: x => x.ApprovedBySecurityObjectID,
                        principalTable: "SecurityObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BasicTasks_SecurityObjects_SecurityObjectID",
                        column: x => x.SecurityObjectID,
                        principalTable: "SecurityObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TestEntity",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CurrentState = table.Column<int>(nullable: false),
                    Message = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestEntity", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BasicTasks_ApprovedBySecurityObjectID",
                table: "BasicTasks",
                column: "ApprovedBySecurityObjectID");

            migrationBuilder.CreateIndex(
                name: "IX_BasicTasks_SecurityObjectID",
                table: "BasicTasks",
                column: "SecurityObjectID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BasicTasks");

            migrationBuilder.DropTable(
                name: "TestEntity");
        }
    }
}
