using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Linq;
using TaskManagerApp.Models;

namespace TaskManagerApp.Controllers
{
    public class TasksController : Controller
    {
        private readonly AppDbContext _db;

        public TasksController(AppDbContext db)
        {
            _db = db;
        }

        // Show all tasks
        public IActionResult Index()
        {
            var tasks = _db.Tasks
                .OrderBy(t => t.IsCompleted)          // incomplete first
                .ThenByDescending(t => t.Priority)    // high priority first
                .ThenBy(t => t.DueDate)              // earlier due date first
                .ToList();

            return View(tasks);
        }


        // Add a new task (POST)
        [HttpPost]
        public IActionResult Add(string title, DateTime? dueDate, TaskPriority priority)
        {
            if (!string.IsNullOrWhiteSpace(title))
            {
                var task = new TaskItem
                {
                    Title = title,
                    IsCompleted = false,
                    DueDate = dueDate,
                    Priority = priority
                };

                _db.Tasks.Add(task);
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        // Mark a task as complete
        public IActionResult Complete(int id)
        {
            var task = _db.Tasks.Find(id);
            if (task != null)
            {
                task.IsCompleted = true;
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // Delete a task
        public IActionResult Delete(int id)
        {
            var task = _db.Tasks.Find(id);
            if (task != null)
            {
                _db.Tasks.Remove(task);
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
