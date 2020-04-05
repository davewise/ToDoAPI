using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace ToDoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ToDoItemsController : ControllerBase
    {
        private readonly ToDoContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ToDoItemsController(ToDoContext context, UserManager<ApplicationUser> user)
        {
            _context = context;
            _userManager = user;
        }

        // GET: api/ToDoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoItemDTO>>> GetToDoItems()
        {
            var user_id = _userManager.GetUserId(HttpContext.User);
            var user = await _context.Users.Include(u => u.Items).FirstOrDefaultAsync(u => u.Id == user_id);

            if (user == null)
            {
                return Unauthorized();
            }

            return user.Items
                .Select(x => ItemToDTO(x))
                .ToList();
        }

        // GET: api/ToDoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoItemDTO>> GetToDoItem(long id)
        {
            var user_id = _userManager.GetUserId(HttpContext.User);
            var user = await _context.Users.Include(u => u.Items).FirstOrDefaultAsync(u => u.Id == user_id);

            if(user == null)
            {
                return Unauthorized();
            }

            var toDoItem = user.Items.SingleOrDefault(t => t.Id == id);
            //var toDoItem = await _context.ToDoItems.SingleAsync(t => t.Id == id); //returns record from DB and serializes into c# class

            if (toDoItem == null)
            {
                return NotFound();
            }

            return ItemToDTO(toDoItem);
        }

        // PUT: api/ToDoItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateToDoItem(long id, ToDoItemDTO toDoItemDTO)
        {
            var user_id = _userManager.GetUserId(HttpContext.User);
            var user = await _context.Users.Include(u => u.Items).FirstOrDefaultAsync(u => u.Id == user_id);

            if (user == null)
            {
                return Unauthorized();
            }

            var toDoItem = user.Items.SingleOrDefault(t => t.Id == id);

            if (id != toDoItem.Id)
            {
                return BadRequest();
            }

            //var toDoItem = await _context.ToDoItems.FindAsync(id);
            if(toDoItem == null)
            {
                return NotFound();
            }

            _context.Entry(toDoItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToDoItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ToDoItems
        [HttpPost]
        public async Task<ActionResult<ToDoItemDTO>> CreateToDoItem(ToDoItemDTO toDoItemDTO)
        {
            var user_id = _userManager.GetUserId(HttpContext.User);
            var user =  await _context.Users.FirstOrDefaultAsync(u => u.Id == user_id);

            var toDoItem = new ToDoItem

            {
                IsComplete = toDoItemDTO.IsComplete,
                Name = toDoItemDTO.Name,
                User = user
        };
            _context.ToDoItems.Add(toDoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetToDoItem),
                new { id = toDoItem.Id },
                ItemToDTO(toDoItem));
        }

        // DELETE: api/ToDoItems/5
        //check user id and block if not correct user
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDoItem(long id)
        {
            var user_id = _userManager.GetUserId(HttpContext.User);
            var user = await _context.Users.Include(u => u.Items).FirstOrDefaultAsync(u => u.Id == user_id);

            if (user == null)
            {
                return Unauthorized();
            }

            var toDoItem = user.Items.SingleOrDefault(t => t.Id == id);

            //var toDoItem = await _context.ToDoItems.FindAsync(id);
            if (toDoItem == null)
            {
                return NotFound();
            }

            _context.ToDoItems.Remove(toDoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ToDoItemExists(long id) =>
            _context.ToDoItems.Any(e => e.Id == id);
        

    private static ToDoItemDTO ItemToDTO(ToDoItem toDoItem) =>
        new ToDoItemDTO
        {
            Id = toDoItem.Id,
            Name = toDoItem.Name,
            IsComplete = toDoItem.IsComplete

        };
    }
}
