using Microsoft.AspNetCore.Mvc;
using MonitorTool.Data;
using MonitorTool.Models;

namespace MonitorTool.Controllers
{
    public class QuizController : Controller
    {

        private readonly MonitorToolDbContext _context;

        public QuizController(MonitorToolDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var questions = _context.Questions
                .Select(q => new QuizViewModel
                {
                    QuestionId = q.Id,
                    QuestionText = q.QuestionText,
                    Options = _context.Options
                        .Where(o => o.QuestionId == q.Id)
                        .Select(o => new QuizOption
                        {
                            Id = o.Id,
                            OptionText = o.OptionText
                        }).ToList()
                }).ToList();

            return View(questions);
        }

        [HttpPost]
        public IActionResult Submit(List<QuizViewModel> model)
        {
            int score = 0;

            foreach (var question in model)
            {
                var correctOptions = _context.Options
                    .Where(o => o.QuestionId == question.QuestionId && o.IsCorrect)
                    .Select(o => o.Id)
                    .ToList();

                var selectedOptions = question.Options.Where(o => o.Selected).Select(o => o.Id).ToList();

                if (!correctOptions.Except(selectedOptions).Any() && correctOptions.Count == selectedOptions.Count)
                {
                    score++;
                }
            }

            ViewBag.Score = score;
            ViewBag.TotalQuestions = model.Count;

            return View("Result");
        }
    }

}
