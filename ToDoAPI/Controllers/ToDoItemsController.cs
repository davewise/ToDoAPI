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
    [Route("api/[controller]/{toDoListId}")]
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
        /// Return a list of ToDoItems for a user given a toDoListId.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/ToDoItems/{toDoListId}
        ///
        /// </remarks>
        /// <returns>A list of ToDoItems</returns>
        /// <response code="201">Returns a list of ToDo items</response>
        /// <response code="401">If the user is not authorized or authenticated</response>            
        // GET: api/ToDoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoItemDTO>>> GetToDoItems(long toDoListId)
        {
           
            //ApplicationUser user = await GetUser();

            var user_id = _userManager.GetUserId(HttpContext.User);

            if (user_id == null)
            {
                return Unauthorized();
            }

            ToDoList toDoList = await _context.ToDoLists.Include(l => l.ToDoItems).SingleOrDefaultAsync(t => t.UserId == user_id && t.Id == toDoListId);
            //ToDoList toDoList = await _context.ToDoLists.Include(l => l.UserId).SingleOrDefaultAsync(t => t.UserId == user_id && t.Id == toDoListId);

            if (toDoList == null)
            {
                return NotFound();
            }

            return toDoList.ToDoItems
                .Select(x => ItemToDTO(x))
                .ToList();
        }

        /// <summary>
        /// Return a particular ToDoItem based on index for a user given a toDoListId.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/ToDoItems/{toDoListId}/{id}
        ///
        /// </remarks>
        /// <returns>A ToDoItem</returns>
        /// <response code="201">Returns the ToDo item</response>
        /// <response code="400">If the item does not exist</response>            
        /// <response code="401">If the user is not authorized or authenticated</response>
        /// <response code="404">If the item is not found</response>
        // GET: api/ToDoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoItemDTO>> GetToDoItem(long id, long toDoListId)
        {
            ApplicationUser user = await GetUser();

            if (user == null)
            {
                return Unauthorized();
            }

            var toDoItem = user.ToDoItems.SingleOrDefault(t => t.Id == id && t.ToDoListId == toDoListId);

            if (toDoItem == null)
            {
                return NotFound();
            }

            return ItemToDTO(toDoItem);
        }

        /// <summary>
        /// Updates a ToDoItem based on index for a user given a toDoListId.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/ToDoItems/{toDoListId}/{id}
        ///     {
        ///        "name": "Item2",
        ///        "isComplete": true
        ///     }
        ///
        /// </remarks>
        /// <returns>An updated ToDoItem</returns>
        /// <response code="201">Returns the updated item</response>
        /// <response code="400">If the item is null</response>            
        /// <response code="401">If the user is not authorized or authenticated</response>            
        /// <response code="404">If the item is not found</response>            
        // PUT: api/ToDoItems/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> UpdateToDoItem(long id, long toDoListId)
        {
            ApplicationUser user = await GetUser();

            if (user == null)
            {
                return Unauthorized();
            }

            var toDoItem = user.ToDoItems.SingleOrDefault(t => t.Id == id && t.ToDoListId == toDoListId);

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
        /// Creates a ToDoItem for a user given a toDoListId.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/ToDoItems/{toDoListId}
        ///     {
        ///        "name": "Item1",
        ///        "isComplete": true
        ///     }
        ///
        /// </remarks>
        /// <returns>A newly created ToDoItem</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>            
        /// <response code="401">If the user is not authorized or authenticated</response>
        // POST: api/ToDoItems
        [HttpPost]
        public async Task<ActionResult<ToDoItemDTO>> CreateToDoItem(ToDoItemDTO toDoItemDTO, long toDoListId)
        {
            ApplicationUser user = await GetUser();

            ToDoList toDoList = await _context.ToDoLists.Include(l => l.ToDoItems).FirstOrDefaultAsync(l => l.Id == toDoListId);


            if (user == null)
            {
                return Unauthorized();
            }

            if(toDoList == null)
            {
                return NotFound();
            }

            var toDoItem = new ToDoItem

            {
                IsComplete = toDoItemDTO.IsComplete,
                Name = toDoItemDTO.Name,
                ToDoList = toDoList,
                ToDoListId = toDoListId,
                User = user,
                UserId = user.Id

            };
            _context.ToDoItems.Add(toDoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetToDoItem),
                new { id = toDoItem.Id, toDoListId = toDoItem.ToDoListId },
                ItemToDTO(toDoItem));
        }

        /// <summary>
        /// Deletes a specific ToDoItem based on index for a user given a toDoListId.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/ToDoItems/{toDoListId}/{id}
        ///
        /// </remarks>
        /// <response code="400">If the item is null</response>            
        /// <response code="401">If the user is not authorized or authenticated</response>            
        /// <response code="404">If the item is not found</response>
        /// DELETE: api/ToDoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDoItem(long id, long toDoListId)
        {
            ApplicationUser user = await GetUser();

            if (user == null)
            {
                return Unauthorized();
            }

            var toDoItem = user.ToDoItems.SingleOrDefault(t => t.Id == id && t.ToDoListId == toDoListId);

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

        private async Task<ApplicationUser> GetUser()
        {
            var user_id = _userManager.GetUserId(HttpContext.User);
            var user = await _context.Users.Include(u => u.ToDoItems).FirstOrDefaultAsync(u => u.Id == user_id);
           
            return user;
        }


        private static ToDoItemDTO ItemToDTO(ToDoItem toDoItem)
        {
            return new ToDoItemDTO
            {
                Id = toDoItem.Id,
                Name = toDoItem.Name,
                IsComplete = toDoItem.IsComplete,
                ToDoListId = toDoItem.ToDoListId
            };
        }
    }
}
