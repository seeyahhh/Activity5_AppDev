using Microsoft.AspNetCore.Mvc;
using Activity5_AppDev.Models;

namespace Activity5_AppDev.Controllers
{
    public class TodoController : Controller
    {
        private readonly AppDbContext _context;

        public TodoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Todo
        public IActionResult Index()
        {
            var items = _context.TodoItems
                .ToList()
                .OrderBy(t => t.IsCompleted)
                .ThenBy(t => t.Deadline)
                .ThenBy(t => t.TimeDeadline)
                .ToList();
            return View(items);
        }

        // POST: /Todo/Add
        [HttpPost]
        public IActionResult Add(string title, string subject, string description,
                                  DateOnly deadline, TimeOnly timeDeadline)
        {
            if (!string.IsNullOrWhiteSpace(title))
            {
                _context.TodoItems.Add(new TodoItem
                {
                    Title = title,
                    Subject = subject,
                    Description = description,
                    Deadline = deadline,
                    TimeDeadline = timeDeadline
                });
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // POST: /Todo/Delete
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var item = _context.TodoItems.Find(id);
            if (item != null)
            {
                _context.TodoItems.Remove(item);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // POST: /Todo/ToggleComplete
        [HttpPost]
        public IActionResult ToggleComplete(int id)
        {
            var item = _context.TodoItems.Find(id);
            if (item != null)
            {
                item.IsCompleted = !item.IsCompleted;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // GET: /Todo/Edit/{id}
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var item = _context.TodoItems.Find(id);
            if (item == null) return NotFound();
            return Json(item);
        }

        // POST: /Todo/Edit
        [HttpPost]
        public IActionResult Edit(int id, string title, string subject, string description,
                                   DateOnly deadline, TimeOnly timeDeadline)
        {
            var item = _context.TodoItems.Find(id);
            if (item != null)
            {
                item.Title = title;
                item.Subject = subject;
                item.Description = description;
                item.Deadline = deadline;
                item.TimeDeadline = timeDeadline;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}