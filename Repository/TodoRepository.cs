using TodoAPI.DB;

namespace TodoAPI.Repository;

public class TodoRepository: ITodoRepository {
    private TodoContext db { get; set; }

    public TodoRepository(TodoContext todoContext) {
        this.db = todoContext;
        this.db.Database.EnsureCreated();
    }

    public async Task<Todo?> GetTodo(int id)
    {
        return await this.db.FindAsync<Todo>(id);
    }

    public async Task<Todo> AddTodo(string description, DateTime? dueDate)
    {
        Todo todo = new Todo {Description = description};
        if (dueDate.HasValue) {
            todo.DueDate = dueDate.Value;
        }
        var result = this.db.Add(todo);
        await this.db.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<bool> SaveTodo(Todo todo)
    {
        this.db.Update<Todo>(todo);
        var result = await this.db.SaveChangesAsync();
        return result > 0;
    }
}