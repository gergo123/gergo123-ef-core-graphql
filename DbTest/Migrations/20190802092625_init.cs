using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DbTest.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "SecurityGroupSecurityIdentity",
                columns: table => new
                {
                    SecurityGroupId = table.Column<long>(nullable: false),
                    SecurityIdentityId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityGroupSecurityIdentity", x => new { x.SecurityGroupId, x.SecurityIdentityId });
                    table.ForeignKey(
                        name: "FK_SecurityGroupSecurityIdentity_SecurityObjects_SecurityGroupId",
                        column: x => x.SecurityGroupId,
                        principalTable: "SecurityObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SecurityGroupSecurityIdentity_SecurityObjects_SecurityIdentityId",
                        column: x => x.SecurityIdentityId,
                        principalTable: "SecurityObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SecurityGroupSecurityIdentity_SecurityIdentityId",
                table: "SecurityGroupSecurityIdentity",
                column: "SecurityIdentityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Placeholder");

            migrationBuilder.DropTable(
                name: "PlaceholderACL");

            migrationBuilder.DropTable(
                name: "SecurityGroupSecurityIdentity");

            migrationBuilder.DropTable(
                name: "SimplePlaceHolders");

            migrationBuilder.DropTable(
                name: "SecurityObjects");
        }
    }
}
