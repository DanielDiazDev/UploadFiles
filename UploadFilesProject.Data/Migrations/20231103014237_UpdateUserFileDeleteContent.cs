using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UploadFilesProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserFileDeleteContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "UserFiles");

            migrationBuilder.RenameColumn(
                name: "Data",
                table: "UserFiles",
                newName: "FileData");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileData",
                table: "UserFiles",
                newName: "Data");

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "UserFiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
