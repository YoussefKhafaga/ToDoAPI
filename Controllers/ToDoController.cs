using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoApi.DTOs.Responses;
using TodoApi.DTOs.ToDoItemDTOs;
using TodoApi.Model;
using TodoApi.Services.ToDoService;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoService _toDoService;
        public ToDoController(IToDoService toDoService)
        {
            _toDoService = toDoService;
        }
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllToDos([FromQuery] int page = 1, [FromQuery] int limit = 10)
        {
            try
            {
                var todos = await _toDoService.GetAllToDosAsync(page, limit);
                if (todos == null || todos.Items == null || !todos.Items.Any())
                    return NotFound("No ToDo items found.");

                return Ok(todos);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("User is not authenticated.");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("No ToDo items found for the current user.");
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetToDoById(int id)
        {
            try
            {
                var todo = await _toDoService.GetToDoByIdAsync(id);
                if (todo == null)
                    return NotFound($"ToDo with ID {id} not found.");
                return Ok(todo);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("User is not authenticated.");
            }
        }
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateToDo([FromBody] ItemCreateDTO toDoItem)
        {
            if (toDoItem == null)
                return BadRequest("ToDo item cannot be null.");
            var createdToDo = await _toDoService.CreateToDoAsync(toDoItem);
            return CreatedAtAction(nameof(GetToDoById), new { id = createdToDo.Id }, createdToDo);
        }
        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteToDo(int id)
        {
            try
            {
                var todo = await _toDoService.GetToDoByIdAsync(id);
                if (todo == null)
                    return NotFound($"ToDo with ID {id} not found.");
                await _toDoService.DeleteToDoAsync(id);
                return NoContent();
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("User is not authorized to delete this item.");
            }
        }
        [HttpPut("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateToDo(int id, [FromBody] ItemUpdateDTO toDoItem)
        {
            if (toDoItem == null)
                return BadRequest("ToDo item cannot be null.");
            var existingToDo = await _toDoService.GetToDoByIdAsync(id);
            if (existingToDo == null)
                return NotFound($"ToDo with ID {id} not found.");
            try
            {
                var updatedToDo = await _toDoService.UpdateToDoAsync(id, toDoItem);
                return Ok(updatedToDo);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("User is not authorized to update this item.");
            }
        }

    }
}
