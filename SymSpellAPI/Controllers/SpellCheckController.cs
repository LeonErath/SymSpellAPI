using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SymSpellAPI.Models;
using System;
namespace SymSpellAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpellCheckController:ControllerBase
    {
        private readonly SpellCheckContext _context;

        public SpellCheckController(SpellCheckContext context)
        {
            _context = context;

            if (_context.SpellCheckItems.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                _context.SpellCheckItems.Add(new SpellCheck { Name = "Item1" });
                _context.SaveChanges();
            }
        }


        // GET: api/Todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpellCheck>>> GetTodoItems()
        {
           
            return await _context.SpellCheckItems.ToListAsync();
        }

        // GET: api/Todo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SpellCheck>> GetTodoItem(long id)
        {
            
            var todoItem = await _context.SpellCheckItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

    }



}
