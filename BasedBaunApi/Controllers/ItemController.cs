using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BasedBaunApi.Models;

namespace BasedBaunApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ItemController : ControllerBase
{
    private readonly ItemContext _context;

    public ItemController(ItemContext context)
    {
        _context = context;
    }

    // GET: api/Item
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Item>>> GetItems()
    {
        if (_context.Items == null) return NotFound();
        return await _context.Items.ToListAsync();
    }

    // GET: api/Item/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Item>> GetItem(int id)
    {
        if (_context.Items == null) return NotFound();
        var item = await _context.Items.FindAsync(id);

        if (item == null) return NotFound();

        return item;
    }

    // PUT: api/Item/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutItem(int itemId, ItemDto.ItemUpdateDto itemUpdateDto)
    {
        try
        {
            var itemComplete = _context.Items.First(i => i.ItemId == itemId);

            itemComplete.Description = itemUpdateDto.Description;
            itemComplete.Lane = itemUpdateDto.Lane;

            _context.Entry(itemComplete).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        catch (ArgumentNullException)
        {
            return NotFound();
        }

        return NoContent();
    }

    // POST: api/Item
    [HttpPost]
    public async Task<ActionResult<Item>> PostItem(ItemDto.ItemCreateDto item)
    {
        if (_context.Items == null) return Problem("Entity set 'ItemContext.Items'  is null.");
        var entityEntry = _context.Items.Add(new Item { Description = item.Description, Lane = item.Lane });
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetItem), new { id = entityEntry.Entity.ItemId }, entityEntry.Entity);
    }

    // DELETE: api/Item/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteItem(long id)
    {
        if (_context.Items == null) return NotFound();
        var item = await _context.Items.FindAsync(id);
        if (item == null) return NotFound();

        _context.Items.Remove(item);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ItemExists(long id)
    {
        return (_context.Items?.Any(e => e.ItemId == id)).GetValueOrDefault();
    }
}