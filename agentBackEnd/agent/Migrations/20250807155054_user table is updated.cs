using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace agent.Migrations
{
    /// <inheritdoc />
    public partial class usertableisupdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVerified",
                table: "AgencyReviews");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "AgencyReviews");

            migrationBuilder.AddColumn<string>(
                name: "ServiceNumber",
                table: "AgencyReviews",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServiceNumber",
                table: "AgencyReviews");

            migrationBuilder.AddColumn<bool>(
                name: "IsVerified",
                table: "AgencyReviews",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "AgencyReviews",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
