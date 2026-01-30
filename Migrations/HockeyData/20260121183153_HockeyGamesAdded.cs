using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ProLab.Migrations.HockeyData
{
    /// <inheritdoc />
    public partial class HockeyGamesAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HockeyGameStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HockeyGameStatusName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HockeyGameStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HockeyPlayers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    SSN = table.Column<string>(type: "text", nullable: true),
                    Age = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HockeyPlayers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HockeySeries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HockeySeriesName = table.Column<string>(type: "text", nullable: true),
                    HockeySeriesShortDescription = table.Column<string>(type: "text", nullable: true),
                    HockeySeriesDescription = table.Column<string>(type: "text", nullable: true),
                    GameTime = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HockeySeries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HockeyTeams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HockeyTeamName = table.Column<string>(type: "text", nullable: true),
                    CoachName = table.Column<string>(type: "text", nullable: true),
                    FoundedYear = table.Column<int>(type: "integer", nullable: false),
                    HockeyArenaId = table.Column<int>(type: "integer", nullable: true),
                    HockeyTeamBadgePath = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HockeyTeams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HockeyTeams_HockeyArenas_HockeyArenaId",
                        column: x => x.HockeyArenaId,
                        principalTable: "HockeyArenas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HockeyGames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GameDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    GameNumber = table.Column<string>(type: "text", nullable: true),
                    HockeyGameStatusId = table.Column<int>(type: "integer", nullable: true),
                    HockeySeriesId = table.Column<int>(type: "integer", nullable: true),
                    HockeyArenaId = table.Column<int>(type: "integer", nullable: true),
                    HockeyTeamId = table.Column<int>(type: "integer", nullable: true),
                    HockeyTeamId1 = table.Column<int>(type: "integer", nullable: true),
                    HomeTeamScore = table.Column<int>(type: "integer", nullable: true),
                    AwayTeamScore = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HockeyGames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HockeyGames_HockeyArenas_HockeyArenaId",
                        column: x => x.HockeyArenaId,
                        principalTable: "HockeyArenas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HockeyGames_HockeyGameStatuses_HockeyGameStatusId",
                        column: x => x.HockeyGameStatusId,
                        principalTable: "HockeyGameStatuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HockeyGames_HockeySeries_HockeySeriesId",
                        column: x => x.HockeySeriesId,
                        principalTable: "HockeySeries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HockeyGames_HockeyTeams_HockeyTeamId",
                        column: x => x.HockeyTeamId,
                        principalTable: "HockeyTeams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HockeyGames_HockeyTeams_HockeyTeamId1",
                        column: x => x.HockeyTeamId1,
                        principalTable: "HockeyTeams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HockeyPlayersHockeyTeam",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HockeyPlayerId = table.Column<int>(type: "integer", nullable: false),
                    HockeyTeamId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HockeyPlayersHockeyTeam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HockeyPlayersHockeyTeam_HockeyPlayers_HockeyPlayerId",
                        column: x => x.HockeyPlayerId,
                        principalTable: "HockeyPlayers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HockeyPlayersHockeyTeam_HockeyTeams_HockeyTeamId",
                        column: x => x.HockeyTeamId,
                        principalTable: "HockeyTeams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HockeyGames_HockeyArenaId",
                table: "HockeyGames",
                column: "HockeyArenaId");

            migrationBuilder.CreateIndex(
                name: "IX_HockeyGames_HockeyGameStatusId",
                table: "HockeyGames",
                column: "HockeyGameStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_HockeyGames_HockeySeriesId",
                table: "HockeyGames",
                column: "HockeySeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_HockeyGames_HockeyTeamId",
                table: "HockeyGames",
                column: "HockeyTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_HockeyGames_HockeyTeamId1",
                table: "HockeyGames",
                column: "HockeyTeamId1");

            migrationBuilder.CreateIndex(
                name: "IX_HockeyPlayersHockeyTeam_HockeyPlayerId",
                table: "HockeyPlayersHockeyTeam",
                column: "HockeyPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_HockeyPlayersHockeyTeam_HockeyTeamId",
                table: "HockeyPlayersHockeyTeam",
                column: "HockeyTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_HockeyTeams_HockeyArenaId",
                table: "HockeyTeams",
                column: "HockeyArenaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HockeyGames");

            migrationBuilder.DropTable(
                name: "HockeyPlayersHockeyTeam");

            migrationBuilder.DropTable(
                name: "HockeyGameStatuses");

            migrationBuilder.DropTable(
                name: "HockeySeries");

            migrationBuilder.DropTable(
                name: "HockeyPlayers");

            migrationBuilder.DropTable(
                name: "HockeyTeams");
        }
    }
}
