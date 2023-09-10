using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoWebService.Models.DTOs.Todo;
using TodoWebService.Providers;
using TodoWebService.Services;

namespace TodoWebService.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TodoController : ControllerBase
    {
        private readonly IRequestUserProvider _userProvider;
        private readonly ITodoService _todoService;

		public TodoController(IRequestUserProvider userProvider, ITodoService todoService)
		{
			_userProvider = userProvider;
			_todoService = todoService;
		}

		[HttpGet("get")]
        public IActionResult Get()
        {
            var userInfo = _userProvider.GetUserInfo();
            return Ok(userInfo);
        }

        [HttpPost("AddTodo")]
        public async Task<IActionResult> Post([FromBody] CreateTodoItemRequest request)
        {
            var userinfo = _userProvider.GetUserInfo();
            var createditem = await _todoService.CreateTodo(userinfo!.id,request);
			return CreatedAtAction(nameof(Get), new { id = createditem!.Id }, createditem);
		}

		[HttpPatch("{id}/status")]
		public async Task<ActionResult<TodoItemDto>> ChangeStatus(int id, [FromBody] bool isCompleted)
		{
			var user = _userProvider.GetUserInfo();

			var todoItem = await _todoService.ChangeTodoItemStatus(user!.id, id, isCompleted);

			return todoItem is not null
				? todoItem : NotFound();
		}

		[HttpDelete("delete/{id}")]
		public async Task<ActionResult<bool>> Delete(int id)
		{
			var user = _userProvider.GetUserInfo();
			var result = await _todoService.DeleteTodo(user!.id, id);
			return result ? result : NotFound();
		}
	}
}
