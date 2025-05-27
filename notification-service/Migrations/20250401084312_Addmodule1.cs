using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace notification_service.Migrations
{
    /// <inheritdoc />
    public partial class Addmodule1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SourceModule",
                table: "Notifications",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SourceModule",
                table: "Notifications");
        }
    }
}
