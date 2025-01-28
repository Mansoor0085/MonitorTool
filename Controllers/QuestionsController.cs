using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using MonitorTool.Models;

namespace MonitorTool.Controllers
{
    public class QuestionsController : Controller
    {
        // Mock data - replace with a database in production
        private static List<Question> questions = new List<Question>
        {
            new Question { Id = 1, Text = "The sky is ____.", Answer = "blue" },
            new Question { Id = 2, Text = "Grass is ____.", Answer = "green" },
            new Question { Id = 3, Text = "Roses are ____.", Answer = "red" }
        };

        // GET: Questions
        public IActionResult Index()
        {
            return View(questions);
        }

        // GET: Questions/Answer/1
        public IActionResult Answer(int id)
        {
            var question = questions.FirstOrDefault(q => q.Id == id);
            if (question == null) return NotFound();

            return View(question);
        }

        // POST: Questions/Answer
        [HttpPost]
        public IActionResult Answer(int id, string userAnswer)
        {
            var question = questions.FirstOrDefault(q => q.Id == id);
            if (question == null) return NotFound();

            question.UserAnswer = userAnswer;
            bool isCorrect = string.Equals(userAnswer, question.Answer, StringComparison.OrdinalIgnoreCase);

            ViewBag.IsCorrect = isCorrect;
            return View("Result", question);
        }
    }
}


