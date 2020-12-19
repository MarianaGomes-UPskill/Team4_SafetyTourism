using Microsoft.EntityFrameworkCore.Migrations;

namespace SafetyTourism.Data.Migrations
{
    public partial class DestinationTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Destinations_CountryID",
                schema: "Identity",
                table: "Destinations",
                column: "CountryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Destinations_Countries_CountryID",
                schema: "Identity",
                table: "Destinations",
                column: "CountryID",
                principalSchema: "Identity",
                principalTable: "Countries",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Destinations_Countries_CountryID",
                schema: "Identity",
                table: "Destinations");

            migrationBuilder.DropIndex(
                name: "IX_Destinations_CountryID",
                schema: "Identity",
                table: "Destinations");
        }
    }
}
