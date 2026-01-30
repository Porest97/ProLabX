namespace ProLab.Areas.ProGym.Models.DataModels
{
    public class ProGymExercise
    {
        public int Id { get; set; }
        public string ProGymExerciseName { get; set; } = string.Empty;
        public string ProGymExerciseDescription { get; set; } = string.Empty;
        public string ProGymExerciseImageUrl { get; set; } = string.Empty;
    }
}