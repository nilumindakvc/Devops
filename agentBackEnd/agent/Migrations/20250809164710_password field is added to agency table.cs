using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace agent.Migrations
{
    /// <inheritdoc />
    public partial class passwordfieldisaddedtoagencytable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Agencies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Agencies");
        }
    }
}
