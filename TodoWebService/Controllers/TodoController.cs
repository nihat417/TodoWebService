using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public TodoController(IRequestUserProvider userProvider)
        {
            _userProvider = userProvider;
        }

        [HttpGet("get")]
        public IActionResult Get()
        {
            var userInfo = _userProvider.GetUserInfo();
            return Ok(userInfo);
        }

        [HttpPost("AddTodo")]
        public IActionResult Post()
        {
            var userinfo = _userProvider.GetUserInfo();
        }
    }
}
