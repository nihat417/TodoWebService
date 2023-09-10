using Microsoft.EntityFrameworkCore;
using TodoWebService.Data;
using TodoWebService.Models.DTOs.Todo;
using TodoWebService.Models.Entities;

namespace TodoWebService.Services
{
    public class TodoService : ITodoService
    {

        private readonly TodoDbContext _context;

        public TodoService(TodoDbContext context)
        {
            _context = context;
        }

        public async Task<TodoItemDto?> ChangeTodoItemStatus(string userId, int id, bool isCompleted)
        {
            var item = await _context.TodoItems.FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);

            if (item is null)
                return null;

            item.IsCompleted = isCompleted;
            item.UpdatedTime = DateTimeOffset.UtcNow;

            await _context.SaveChangesAsync();
            return new TodoItemDto
            {
                Id = item.Id,
                Text = item.Text!,
                CreatedAt = item.CreatedTime,
                IsComleted = item.IsCompleted
            };
        }

        public async Task<TodoItemDto?> CreateTodo(string userId, CreateTodoItemRequest request)
        {
            var user = await _context.Users.FindAsync(userId) ?? throw new KeyNotFoundException();

            var now = DateTimeOffset.UtcNow;

            var item = new TodoItem
            {
                Text = request.Text,
                CreatedTime = now,
                UpdatedTime = now,
                IsCompleted = false,
                UserId = userId
            };
            item = _context.TodoItems.Add(item).Entity;
            await _context.SaveChangesAsync();

            return new TodoItemDto
            {
                Id = item.Id,
                Text = item.Text!,
                CreatedAt = item.CreatedTime,
                IsComleted = item.IsCompleted
            };
        }

        public Task<bool> DeleteTodo(string userId, int id)
        {
            throw new NotImplementedException();
        }

        public Task<TodoItemDto?> GetTodoItem(string userId, int id)
        {
            throw new NotImplementedException();
        }
    }
}
