
namespace MonitorTool.Models
{
    public class MCQuestions
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public List<MCOptions> Options { get; set; }
    }
}
