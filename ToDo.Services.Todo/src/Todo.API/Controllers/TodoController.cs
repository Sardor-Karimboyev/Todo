using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Todo.API.Commands;
using Todo.API.Models.ViewModels;
using Todo.API.Queries;
using ToDo.Common.Dispatchers;

namespace Todo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TodoController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;
        public TodoController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        // GET: api/Todo
        [HttpGet]
        public async Task<IActionResult> Collection([FromQuery] BrowseTodos query)
            => Ok(await _dispatcher.QueryAsync(query));

        // GET: api/Todo/5
        [HttpGet("{Id}")]
        public async Task<ActionResult<TodoViewModel>> Get([FromRoute] GetTodo query)
        {
            var todo = await _dispatcher.QueryAsync(query);
            if (todo is null)
            {
                return NotFound();
            }

            return todo;
        }

        // POST: api/Todo
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateTodo command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _dispatcher.SendAsync(command);

            return Accepted();
        }

        // PUT: api/Todo/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateTodo command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _dispatcher.SendAsync(command);
            return Ok();
        }

        // PUT: api/Todo/5
        [HttpPut("ChangeIsDone")]
        public async Task<IActionResult> ChangeIsDone([FromBody] ChangeIsDoneTodo command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _dispatcher.SendAsync(command);
            return Ok();
        }
        
        // PUT: api/Todo/5
        [HttpPut("ChangeOrder")]
        public async Task<IActionResult> ChangeOrder([FromBody] ChangeOrderTodo command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _dispatcher.SendAsync(command);
            return Ok();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _dispatcher.SendAsync(new DeleteTodo(id));
            return Accepted();
        }
    }
}
