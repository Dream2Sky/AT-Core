using System.Collections.Generic;
using System.Linq;
using AT_Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace AT_Core.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _context;
        public TodoController(TodoContext context)
        {
            _context = context;
            if (_context.TodoItems.Count() <= 0)
            {
                _context.TodoItems.Add(new TodoItem()
                {
                    Name = "hehe"
                });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public ActionResult<IList<TodoItem>> GetAll()
        {
            return _context.TodoItems.ToList();
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public ActionResult<TodoItem> GetById(long Id)
        {
            var item = _context.TodoItems.Find(Id);
            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        [HttpPost]
        public IActionResult Create(TodoItem item)
        {
            _context.TodoItems.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetTodo", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, TodoItem item)
        {
            var todo = _context.TodoItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }
            todo.IsComplete = item.IsComplete;
            todo.Name = item.Name;

            _context.TodoItems.Update(todo);
            _context.SaveChanges();

            return CreatedAtRoute("GetTodo", new { id = item.Id }, item);
        }
    }
}