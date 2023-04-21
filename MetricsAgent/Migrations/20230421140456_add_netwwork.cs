using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MetricsAgent.Migrations
{
    /// <inheritdoc />
    public partial class add_netwwork : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "networkMetrics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Value = table.Column<int>(type: "INTEGER", nullable: false),
                    Time = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_networkMetrics", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "networkMetrics");
        }
    }
}
