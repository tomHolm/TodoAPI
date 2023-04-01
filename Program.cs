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
    "/todo",
    async (ITodoRepository repository, Todo todo) => {
        var result = await repository.SaveTodo(todo);
        return result
            ? Results.NoContent()
            : Results.NotFound(); 
    }
);
app.MapDelete(
    "/todo/{id}",
    async (ITodoRepository repository, int id) => {
        var result = await repository.DeleteTodo(id);
        return result
            ? Results.NoContent()
            : Results.NotFound();
    }
);
app.MapGet(
    "/todo/list",
    async (ITodoRepository repository) => {
        return Results.Ok(await repository.GetList());
    }
);
app.MapGet(
    "/todo/list/{isDone}",
    async (ITodoRepository repository, bool isDone) => {
        return Results.Ok(await repository.GetList(isDone));
    }
);

app.Run();
