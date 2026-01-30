using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ProLab.Migrations.ProGym
{
    /// <inheritdoc />
    public partial class ProGymExercisesAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProGymExercises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProGymExerciseName = table.Column<string>(type: "text", nullable: true),
                    ProGymExerciseDescription = table.Column<string>(type: "text", nullable: true),
                    ProGymAxerciseImageUrl = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProGymExercises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProGymWorkoutExercises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProGymWorkOutId = table.Column<int>(type: "integer", nullable: false),
                    ProGymExerciseId = table.Column<int>(type: "integer", nullable: false),
                    Sets = table.Column<int>(type: "integer", nullable: false),
                    Reps = table.Column<int>(type: "integer", nullable: false),
                    Weight = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProGymWorkoutExercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProGymWorkoutExercises_ProGymExercises_ProGymExerciseId",
                        column: x => x.ProGymExerciseId,
                        principalTable: "ProGymExercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProGymWorkoutExercises_ProGymWorkOuts_ProGymWorkOutId",
                        column: x => x.ProGymWorkOutId,
                        principalTable: "ProGymWorkOuts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProGymWorkoutExercises_ProGymExerciseId",
                table: "ProGymWorkoutExercises",
                column: "ProGymExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_ProGymWorkoutExercises_ProGymWorkOutId",
                table: "ProGymWorkoutExercises",
                column: "ProGymWorkOutId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProGymWorkoutExercises");

            migrationBuilder.DropTable(
                name: "ProGymExercises");
        }
    }
}