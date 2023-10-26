using System.ComponentModel.DataAnnotations;

namespace AlexParallelismApp.DAL.Models;

public class XEntity
{
    [Key] public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime UpdateTime { get; set; }
}