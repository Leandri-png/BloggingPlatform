using Microsoft.EntityFrameworkCore.Migrations;

namespace BloggingPlatform.DataService.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    postId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"), // Use SQL Server identity for auto-increment
                    title = table.Column<string>(maxLength: 200, nullable: false),
                    description = table.Column<string>(nullable: false), // Adjust data type if necessary
                    publication_date = table.Column<DateTime>(nullable: false), // Adjust data type if necessary
                    user = table.Column<string>(nullable: false) // Adjust data type if necessary
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.postId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Post");
        }
    }
}

