using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BizManWebRC2.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rounds",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rounds", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Golfers",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Handicap = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    TeamID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Golfers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Golfers_Teams_TeamID",
                        column: x => x.TeamID,
                        principalTable: "Teams",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MatchRoundID = table.Column<int>(nullable: false),
                    TeamOneID = table.Column<int>(nullable: true),
                    TeamTwoID = table.Column<int>(nullable: true),
                    TeeOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Matches_Rounds_MatchRoundID",
                        column: x => x.MatchRoundID,
                        principalTable: "Rounds",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Matches_Teams_TeamOneID",
                        column: x => x.TeamOneID,
                        principalTable: "Teams",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_Teams_TeamTwoID",
                        column: x => x.TeamTwoID,
                        principalTable: "Teams",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Logins",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: false),
                    GolferID = table.Column<int>(nullable: true),
                    Identifier = table.Column<string>(nullable: false),
                    LoginType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logins", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Logins_Golfers_GolferID",
                        column: x => x.GolferID,
                        principalTable: "Golfers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Scores",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GolferID = table.Column<int>(nullable: false),
                    MatchID = table.Column<int>(nullable: false),
                    Score = table.Column<int>(nullable: false),
                    SubbedForID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scores", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Scores_Golfers_GolferID",
                        column: x => x.GolferID,
                        principalTable: "Golfers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Scores_Matches_MatchID",
                        column: x => x.MatchID,
                        principalTable: "Matches",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Scores_Golfers_SubbedForID",
                        column: x => x.SubbedForID,
                        principalTable: "Golfers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Golfers_TeamID",
                table: "Golfers",
                column: "TeamID");

            migrationBuilder.CreateIndex(
                name: "IX_Scores_GolferID",
                table: "Scores",
                column: "GolferID");

            migrationBuilder.CreateIndex(
                name: "IX_Scores_MatchID",
                table: "Scores",
                column: "MatchID");

            migrationBuilder.CreateIndex(
                name: "IX_Scores_SubbedForID",
                table: "Scores",
                column: "SubbedForID");

            migrationBuilder.CreateIndex(
                name: "IX_Logins_GolferID",
                table: "Logins",
                column: "GolferID");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_MatchRoundID",
                table: "Matches",
                column: "MatchRoundID");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_TeamOneID",
                table: "Matches",
                column: "TeamOneID");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_TeamTwoID",
                table: "Matches",
                column: "TeamTwoID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Scores");

            migrationBuilder.DropTable(
                name: "Logins");

            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "Golfers");

            migrationBuilder.DropTable(
                name: "Rounds");

            migrationBuilder.DropTable(
                name: "Teams");
        }
    }
}
