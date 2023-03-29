using TodoAPI.DB;

namespace TodoAPI.Repository;

public interface ITodoRepository {
    public Task<Todo?> GetTodo(int id);
    public Task<Todo> AddTodo(string description, DateTime? dueDate);
}