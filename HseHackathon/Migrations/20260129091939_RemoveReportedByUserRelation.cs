using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HseHackathon.Migrations
{
    /// <inheritdoc />
    public partial class RemoveReportedByUserRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Incidents_Users_ReportedByUserId",
                table: "Incidents");

            migrationBuilder.DropIndex(
                name: "IX_Incidents_ReportedByUserId",
                table: "Incidents");

            migrationBuilder.DropColumn(
                name: "ReportedByUserId",
                table: "Incidents");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ReportedByUserId",
                table: "Incidents",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_ReportedByUserId",
                table: "Incidents",
                column: "ReportedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Incidents_Users_ReportedByUserId",
                table: "Incidents",
                column: "ReportedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
