using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Test.Db.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "Placeholder",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LocalString = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Count = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Placeholder", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlaceholderACL",
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
                    table.PrimaryKey("PK_PlaceholderACL", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SecurityObjects",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Discriminator = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Identifier = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityObjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SimplePlaceHolders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SimpleProperty = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimplePlaceHolders", x => x.Id);
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

            migrationBuilder.CreateTable(
                name: "BasicTasks",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EntityId = table.Column<long>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    UniqueID = table.Column<string>(nullable: true),
                    ApprovedAt = table.Column<DateTime>(nullable: false),
                    ApprovedBySecurityIdentityID = table.Column<long>(nullable: true),
                    UserCreated = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    UserModified = table.Column<string>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    SecurityGroupId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasicTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BasicTasks_SecurityObjects_ApprovedBySecurityIdentityID",
                        column: x => x.ApprovedBySecurityIdentityID,
                        principalTable: "SecurityObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BasicTasks_SecurityObjects_SecurityGroupId",
                        column: x => x.SecurityGroupId,
                        principalTable: "SecurityObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SecurityGroupSecurityIdentities",
                columns: table => new
                {
                    SecurityGroupId = table.Column<long>(nullable: false),
                    SecurityIdentityId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityGroupSecurityIdentities", x => new { x.SecurityGroupId, x.SecurityIdentityId });
                    table.ForeignKey(
                        name: "FK_SecurityGroupSecurityIdentities_SecurityObjects_SecurityGroupId",
                        column: x => x.SecurityGroupId,
                        principalTable: "SecurityObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SecurityGroupSecurityIdentities_SecurityObjects_SecurityIdentityId",
                        column: x => x.SecurityIdentityId,
                        principalTable: "SecurityObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "SecurityObjects",
                columns: new[] { "Id", "Discriminator", "Name" },
                values: new object[] { 1L, "SecurityGroup", "Administrators" });

            migrationBuilder.CreateIndex(
                name: "IX_BasicTasks_ApprovedBySecurityIdentityID",
                table: "BasicTasks",
                column: "ApprovedBySecurityIdentityID");

            migrationBuilder.CreateIndex(
                name: "IX_BasicTasks_SecurityGroupId",
                table: "BasicTasks",
                column: "SecurityGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityGroupSecurityIdentities_SecurityIdentityId",
                table: "SecurityGroupSecurityIdentities",
                column: "SecurityIdentityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BasicTaskAcl");

            migrationBuilder.DropTable(
                name: "BasicTasks");

            migrationBuilder.DropTable(
                name: "Placeholder");

            migrationBuilder.DropTable(
                name: "PlaceholderACL");

            migrationBuilder.DropTable(
                name: "SecurityGroupSecurityIdentities");

            migrationBuilder.DropTable(
                name: "SimplePlaceHolders");

            migrationBuilder.DropTable(
                name: "TestEntity");

            migrationBuilder.DropTable(
                name: "SecurityObjects");
        }
    }
}
