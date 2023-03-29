using TodoAPI.DB;
using Microsoft.EntityFrameworkCore;
using TodoAPI.Repository;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TodoContext>(opts => opts.UseSqlite("Filename=TodosDB.db"));
builder.Services.AddScoped<ITodoRepository, TodoRepository>();

var app = builder.Build();
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet(
    "/todo/{id}",
    async (ITodoRepository repository, int id) => {
        var result = await repository.GetTodo(id);
        return result != null
            ? Results.Ok(result)
            : Results.NotFound();
    }
);
app.MapPost(
    "/todo",
    async (ITodoRepository repository, string description, DateTime? dueDate) => {
        var result = await repository.AddTodo(description, dueDate);
        return Results.Created<Todo>($"/todo/{result.Id}", result);
    }
);
app.MapPut(
    "/todo/{id}",
    async (ITodoRepository repository, int id, bool isDone) => {
        var result = await repository.GetTodo(id);
        if (result == null) {
            return Results.NotFound();
        }
        if (result.IsDone != isDone) {
            result.IsDone = isDone;
            await repository.SaveTodo(result);
        }
        return Results.NoContent(); 
    }
);

app.Run();
