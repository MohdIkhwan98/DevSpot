using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevSpot.Migrations
{
    /// <inheritdoc />
    public partial class SyncModelChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PosteddDate",
                table: "JobPostings",
                newName: "PostedDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PostedDate",
                table: "JobPostings",
                newName: "PosteddDate");
        }
    }
}
