using TodoWebService.Models.DTOs.Todo;

namespace TodoWebService.Services
{
    public interface ITodoService
    {
        Task<TodoItemDto?> GetTodoItem(string userId, int id);
        Task<TodoItemDto?> ChangeTodoItemStatus(string userId, int id, bool isCompleted);
        Task<TodoItemDto?> CreateTodo(string userId, CreateTodoItemRequest request);
        Task<bool> DeleteTodo(string userId, int id);
        // get todos with pagination, with search
    }
}
