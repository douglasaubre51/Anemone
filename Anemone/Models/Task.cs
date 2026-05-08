namespace Anemone.Models;

public class Task
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public string Content { get; set; } = string.Empty;
    public bool IsDone { get; set; }

    public string StatusColor { get; set; } = "Red";
}
