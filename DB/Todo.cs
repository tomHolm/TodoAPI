namespace TodoAPI.DB;

public class Todo {
    public int Id { get; set; }
    public string Description { get; set; }
    public bool IsDone { get; set; }
    public DateTime? DueDate { get; set; }
}