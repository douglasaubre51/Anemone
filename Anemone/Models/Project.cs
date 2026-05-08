namespace Anemone.Models;

public class Project
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string ShortDesc { get; set; } = string.Empty;
    public string ProjectSpec { get; set; } = string.Empty;

    public int UnfinishedTasksNo { get; set; }
    public bool IsTaskAvailable { get; set; }
}
