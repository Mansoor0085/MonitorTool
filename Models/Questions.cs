namespace MonitorTool.Models
{
    public class Question
    {
        public int Id { get; set; }

        public string? Text { get; set; }  // The question text with a blank.
        public string? Answer { get; set; } // The correct answer.
        public string? UserAnswer { get; set; } // The user's submitted answer.
    }
}
