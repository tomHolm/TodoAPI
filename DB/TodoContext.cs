using Microsoft.EntityFrameworkCore;

namespace TodoAPI.DB;

public class TodoContext: DbContext {
    public DbSet<Todo> Todos { get; set; }
    public TodoContext(DbContextOptions<TodoContext> opts): base(opts) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Todo>().ToTable("todos");
        modelBuilder.Entity<Todo>(
            entity => {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasKey(e => e.Id);
            }
        );
        base.OnModelCreating(modelBuilder);
    }
}