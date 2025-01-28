using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MonitorTool.Data;
using MonitorTool.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MonitorTool.Models;

namespace MonitorTool.Controllers
{
    public class StudentController : Controller
    {
        private readonly ILogger<StudentController> _logger;
        public readonly MonitorToolDbContext _context;

        public StudentController(ILogger<StudentController> logger, MonitorToolDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var students = _context.Students.ToList();
            return View(students);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("Id,Name,Gender,Age,Email")] Student student)
        {
            if(ModelState.IsValid)
            {
                _context.Students.Add(student);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(student);
        }

        public IActionResult Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var student = _context.Students.FirstOrDefault(x => x.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }


        [HttpPost]
        public IActionResult Edit([Bind("Id,Name,Gender,Age,Email")] Student student)
        {
            if(student.Id !=null)
            {
                _context.Students.Update(student);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }     
                
            return View(student);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var student = _context.Students.FirstOrDefault(y => y.Id == id);
            if(student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost]
        public IActionResult Delete([Bind("Id,Name,Gender,Age,Email")] Student student)
        {
            if(student==null)
                return NotFound();

            _context.Students.Remove(student);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
