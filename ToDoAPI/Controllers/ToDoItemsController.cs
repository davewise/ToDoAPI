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
    [Produces("application/json")]
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

        /// <summary>
        /// Return a list of ToDoItems for logged in user.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/ToDoItems
        ///
        /// </remarks>
        /// <returns>A list of ToDoItems</returns>
        /// <response code="201">Returns a list of ToDo items</response>
        /// <response code="401">If the user is not authorized or authenticated</response>            
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

        /// <summary>
        /// Return a particular ToDoItem based on index for logged in user.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/ToDoItems/{id}
        ///
        /// </remarks>
        /// <returns>A ToDoItem</returns>
        /// <response code="201">Returns the ToDo item</response>
        /// <response code="400">If the item does not exist</response>            
        /// <response code="401">If the user is not authorized or authenticated</response>
        /// <response code="404">If the item is not found</response>
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

            if (toDoItem == null)
            {
                return NotFound();
            }

            return ItemToDTO(toDoItem);
        }

        /// <summary>
        /// Updates a ToDoItem based on index for logged in user.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/ToDoItems/{id}
        ///     {
        ///        "name": "Item2",
        ///        "isComplete": true
        ///     }
        ///
        /// </remarks>
        /// <param name="item"></param>
        /// <returns>An updated ToDoItem</returns>
        /// <response code="201">Returns the updated item</response>
        /// <response code="400">If the item is null</response>            
        /// <response code="401">If the user is not authorized or authenticated</response>            
        /// <response code="404">If the item is not found</response>            
        // PUT: api/ToDoItems/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
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

        /// <summary>
        /// Creates a ToDoItem for logged in user.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/ToDoItems
        ///     {
        ///        "name": "Item1",
        ///        "isComplete": true
        ///     }
        ///
        /// </remarks>
        /// <param name="item"></param>
        /// <returns>A newly created ToDoItem</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>            
        /// <response code="401">If the user is not logged in</response>
        // POST: api/ToDoItems
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<ToDoItemDTO>> CreateToDoItem(ToDoItemDTO toDoItemDTO)
        {
            var user_id = _userManager.GetUserId(HttpContext.User);
            var user =  await _context.Users.FirstOrDefaultAsync(u => u.Id == user_id);

            if (user == null)
            {
                return Unauthorized();
            }

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

        /// <summary>
        /// Deletes a specific ToDoItem based on index for logged in user.
        /// </summary>
        /// <param name="id"></param>        
        /// <response code="400">If the item is null</response>            
        /// <response code="401">If the user is not authorized or authenticated</response>            
        /// <response code="404">If the item is not found</response>
        /// DELETE: api/ToDoItems/5
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
