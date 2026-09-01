using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using render_ci.Data;
using render_ci.Models;

namespace render_ci.Controllers;

[ApiController]
[Route("api/todo")]
public class TodoController : ControllerBase
{
    private readonly TodoContext _context;

    public TodoController(TodoContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Models.TodoItem>>> GetAll()
    {
        return await _context.TodoItems.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Models.TodoItem>> GetById(int id)
    {
        var item = await _context.TodoItems.FindAsync(id);
        if (item == null)
        {
            return NotFound();
        }
        return item;
    }

    [HttpPost]
    public async Task<ActionResult<Models.TodoItem>> Create([FromBody] Models.TodoItem newItem)
    {
        _context.TodoItems.Add(newItem);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = newItem.Id }, newItem);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Models.TodoItem updatedItem)
    {
        if (id != updatedItem.Id)
        {
            return BadRequest();
        }
        var item = await _context.TodoItems.FindAsync(id);
        if (item == null)
        {
            return NotFound();
        }
        item.Title = updatedItem.Title;
        item.IsCompleted = updatedItem.IsCompleted;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.TodoItems.FindAsync(id);
        if (item == null)
        {
            return NotFound();
        }
        _context.TodoItems.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}