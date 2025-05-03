using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaranSoft.MyGarage.Repository.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class Update1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ManufacturerName",
                table: "Manufacturers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Manufacturers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ManufacturerCountryId",
                table: "Manufacturers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "YearCreation",
                table: "Manufacturers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Manufacturers_CountryId",
                table: "Manufacturers",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Manufacturers_ManufacturerCountryId",
                table: "Manufacturers",
                column: "ManufacturerCountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Manufacturers_Countries_CountryId",
                table: "Manufacturers",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Manufacturers_Countries_ManufacturerCountryId",
                table: "Manufacturers",
                column: "ManufacturerCountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Manufacturers_Countries_CountryId",
                table: "Manufacturers");

            migrationBuilder.DropForeignKey(
                name: "FK_Manufacturers_Countries_ManufacturerCountryId",
                table: "Manufacturers");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropIndex(
                name: "IX_Manufacturers_CountryId",
                table: "Manufacturers");

            migrationBuilder.DropIndex(
                name: "IX_Manufacturers_ManufacturerCountryId",
                table: "Manufacturers");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Manufacturers");

            migrationBuilder.DropColumn(
                name: "ManufacturerCountryId",
                table: "Manufacturers");

            migrationBuilder.DropColumn(
                name: "YearCreation",
                table: "Manufacturers");

            migrationBuilder.AlterColumn<int>(
                name: "ManufacturerName",
                table: "Manufacturers",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
