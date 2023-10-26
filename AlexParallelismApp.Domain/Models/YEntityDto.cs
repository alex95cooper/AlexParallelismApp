namespace AlexParallelismApp.Domain.Models;

public class YEntityDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsLocked { get; set; }
    public string SessionId { get; set; }
}