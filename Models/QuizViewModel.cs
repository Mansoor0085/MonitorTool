using Microsoft.VisualBasic.FileIO;

namespace MonitorTool.Models
{
    public class QuizViewModel
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public List<QuizOption> Options { get; set; }
    }
}
