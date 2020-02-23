using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SSOauth.Migrations
{
    public partial class SSOauth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    login = table.Column<string>(nullable: true),
                    password = table.Column<string>(nullable: true),
                    claim = table.Column<string>(nullable: true),
                    pepper = table.Column<string>(nullable: true),
                    created_at = table.Column<DateTime>(nullable: false),
                    signature = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
