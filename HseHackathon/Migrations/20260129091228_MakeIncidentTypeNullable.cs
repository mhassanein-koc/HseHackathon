using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HseHackathon.Migrations
{
    /// <inheritdoc />
    public partial class MakeIncidentTypeNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Incidents_IncidentTypes_IncidentTypeId",
                table: "Incidents");

            migrationBuilder.AlterColumn<int>(
                name: "IncidentTypeId",
                table: "Incidents",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Incidents_IncidentTypes_IncidentTypeId",
                table: "Incidents",
                column: "IncidentTypeId",
                principalTable: "IncidentTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Incidents_IncidentTypes_IncidentTypeId",
                table: "Incidents");

            migrationBuilder.AlterColumn<int>(
                name: "IncidentTypeId",
                table: "Incidents",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Incidents_IncidentTypes_IncidentTypeId",
                table: "Incidents",
                column: "IncidentTypeId",
                principalTable: "IncidentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
