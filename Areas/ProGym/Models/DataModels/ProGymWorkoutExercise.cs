namespace ProLab.Areas.ProGym.Models.DataModels
{
    public class ProGymWorkoutExercise
    {
        public int Id { get; set; }

        // Relationer
        public int ProGymWorkOutId { get; set; }
        public ProGymWorkOut ProGymWorkOut { get; set; } = null!;

        public int ProGymExerciseId { get; set; }
        public ProGymExercise ProGymExercise { get; set; } = null!;

        // Utförande
        public int Sets { get; set; }
        public int Reps { get; set; }
        public decimal Weight { get; set; }   // kg
    }
}