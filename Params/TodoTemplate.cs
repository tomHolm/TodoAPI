namespace TodoAPI.Params;

public class TodoTemplate {
    public string Description { get; set; } = string.Empty;
    public DateTime? DueDate { get; set; }
}