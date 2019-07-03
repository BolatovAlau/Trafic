using Microsoft.EntityFrameworkCore.Migrations;

namespace TraficLight.BusinessLogic.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sequences",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Start = table.Column<string>(nullable: true),
                    StartNum = table.Column<int>(nullable: false),
                    FirstMissing = table.Column<int>(nullable: false),
                    SecondMissing = table.Column<int>(nullable: false),
                    IsNotFirst = table.Column<bool>(nullable: false),
                    Broken = table.Column<bool>(nullable: false),
                    CurentDeep = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sequences", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sequences");
        }
    }
}
