using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Teltonika.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Covid19Cases",
                columns: table => new
                {
                    object_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    gender = table.Column<string>(nullable: true),
                    age_bracket = table.Column<string>(nullable: true),
                    municipality_name = table.Column<string>(nullable: true),
                    municipality_code = table.Column<string>(nullable: true),
                    confirmation_date = table.Column<DateTimeOffset>(nullable: false),
                    case_code = table.Column<string>(nullable: true),
                    X = table.Column<float>(nullable: true),
                    Y = table.Column<float>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Covid19Cases", x => x.object_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Covid19Cases");
        }
    }
}
