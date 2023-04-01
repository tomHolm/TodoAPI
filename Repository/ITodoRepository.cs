using TodoAPI.DB;

namespace TodoAPI.Repository;

public interface ITodoRepository {
    public Task<Todo?> GetTodo(int id);
    public Task<Todo> AddTodo(string description, DateTime? dueDate);
    public Task<Boolean> SaveTodo(Todo todo);
    public Task<Boolean> DeleteTodo(int id);
    public Task<IEnumerable<Todo>> GetList(bool? isDone = null);
}