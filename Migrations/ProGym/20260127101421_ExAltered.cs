using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProLab.Migrations.ProGym
{
    /// <inheritdoc />
    public partial class ExAltered : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProGymAxerciseImageUrl",
                table: "ProGymExercises",
                newName: "ProGymExerciseImageUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProGymExerciseImageUrl",
                table: "ProGymExercises",
                newName: "ProGymAxerciseImageUrl");
        }
    }
}
