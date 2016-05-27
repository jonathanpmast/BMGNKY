using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BizManWebRC2.Migrations
{
    public partial class MatchTeamsRefactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Teams_TeamOneID",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Teams_TeamTwoID",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_TeamOneID",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_TeamTwoID",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "TeamOneID",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "TeamTwoID",
                table: "Matches");

            migrationBuilder.CreateTable(
                name: "MatchTeams",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MatchID = table.Column<int>(nullable: true),
                    TeamID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchTeams", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MatchTeams_Matches_MatchID",
                        column: x => x.MatchID,
                        principalTable: "Matches",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MatchTeams_Teams_TeamID",
                        column: x => x.TeamID,
                        principalTable: "Teams",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MatchTeams_MatchID",
                table: "MatchTeams",
                column: "MatchID");

            migrationBuilder.CreateIndex(
                name: "IX_MatchTeams_TeamID",
                table: "MatchTeams",
                column: "TeamID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MatchTeams");

            migrationBuilder.AddColumn<int>(
                name: "TeamOneID",
                table: "Matches",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TeamTwoID",
                table: "Matches",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Matches_TeamOneID",
                table: "Matches",
                column: "TeamOneID");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_TeamTwoID",
                table: "Matches",
                column: "TeamTwoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Teams_TeamOneID",
                table: "Matches",
                column: "TeamOneID",
                principalTable: "Teams",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Teams_TeamTwoID",
                table: "Matches",
                column: "TeamTwoID",
                principalTable: "Teams",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
