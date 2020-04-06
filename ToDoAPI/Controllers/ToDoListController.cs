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
    public class ToDoListsController : ControllerBase
    {
        private readonly ToDoContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ToDoListsController(ToDoContext context, UserManager<ApplicationUser> user)
        {
            _context = context;
            _userManager = user;
        }

        /// <summary>
        /// Return a list of ToDoLists for a user.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/ToDoLists
        ///
        /// </remarks>
        /// <returns>A list of ToDoLists</returns>
        /// <response code="201">Returns a list of ToDo lists</response>
        /// <response code="401">If the user is not authorized or authenticated</response>            
        // GET: api/ToDoLists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoListDTO>>> GetToDoLists()
        {

            var user = await GetUser();

            if (user == null)
            {
                return Unauthorized();
            }

            return user.ToDoLists
                .Select(x => ListToDTO(x))
                .ToList();
        }

        /// <summary>
        /// Return a ToDoList for a user given a toDoListId.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/ToDoLists/{id}
        ///
        /// </remarks>
        /// <returns>A ToDoList</returns>
        /// <response code="201">Returns a ToDo list</response>
        /// <response code="401">If the user is not authorized or authenticated</response>
        /// <response code="404">If the item is not found</response>
        // GET: api/ToDoList/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<ToDoListDTO>> GetToDoList(long id)
        {
            var user = await GetUser();

            if (user == null)
            {
                return Unauthorized();
            }

            var toDoList = user.ToDoLists.SingleOrDefault(t => t.Id == id);

            if(toDoList == null)
            {
                return NotFound();
            }

            return ListToDTO(toDoList);
        }

        /// <summary>
        /// Creates a ToDoList for a user.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/ToDoLists
        ///
        /// </remarks>
        /// <returns>A newly created ToDoList</returns>
        /// <response code="201">Returns the newly created ToDoList</response>
        /// <response code="400">If the item is null</response>            
        /// <response code="401">If the user is not authorized or authenticated</response>
        // POST: api/ToDoList
        [HttpPost]
        public async Task<ActionResult<ToDoListDTO>> CeateToDoList(ToDoListDTO toDoListDTO)
        {
            ApplicationUser user = await GetUser();

            if (user == null)
            {
                return Unauthorized();
            }

            var toDoList = new ToDoList
            {
                Name = toDoListDTO.Name,
                User = user,
                UserId = user.Id
            };

            _context.ToDoLists.Add(toDoList);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetToDoList),
                new { id = toDoList.Id },
                ListToDTO(toDoList));
        }

        /// <summary>
        /// Updates a ToDoList for a user given a toDoListId.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/ToDoLists/{id}
        ///
        /// </remarks>
        /// <returns>An updated ToDoList</returns>
        /// <response code="201">Returns the updated ToDoList</response>
        /// <response code="400">If the item is null</response>            
        /// <response code="401">If the user is not authorized or authenticated</response>
        /// <response code="404">If the item is not found</response>            
        // PUT: api/ToDoList/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UdateToDoList(long id, ToDoListDTO toDoListDTO)
        {
            ApplicationUser user = await GetUser();

            if(user == null)
            {
                return Unauthorized();
            }

            var toDoList = user.ToDoLists.SingleOrDefault(t => t.Id == id);

            if (id != toDoList.Id)
            {
                return BadRequest();
            }

            if(toDoList == null)
            {
                return NotFound();
            }

            toDoList.Name = toDoListDTO.Name;

            _context.Entry(toDoList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToDoListExists(id))
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
        /// Deletes a ToDoList for a user given a toDoListId.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/ToDoLists/{id}
        ///
        /// </remarks>
        /// <response code="400">If the item is null</response>            
        /// <response code="401">If the user is not authorized or authenticated</response>            
        /// <response code="404">If the item is not found</response>
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDoList(long id)
        {
            ApplicationUser user = await GetUser();

            if(user == null)
            {
                return Unauthorized();
            }

            var toDoList = user.ToDoLists.SingleOrDefault(t => t.Id == id);

            if (toDoList == null)
            {
                return NotFound();
            }

            _context.ToDoLists.Remove(toDoList);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ToDoListExists(long id)
        {
            return _context.ToDoLists.Any(e => e.Id == id);
        }

        private async Task<ApplicationUser> GetUser()
        {
            var user_id = _userManager.GetUserId(HttpContext.User);
            var user = await _context.Users.Include(u => u.ToDoLists).FirstOrDefaultAsync(u => u.Id == user_id);

            return user;
        }

        private static ToDoListDTO ListToDTO(ToDoList toDoList)
        {
            return new ToDoListDTO
            {
                Id = toDoList.Id,
                Name = toDoList.Name
            };
        }
    }
}
