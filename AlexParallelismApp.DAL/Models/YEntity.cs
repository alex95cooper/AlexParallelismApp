using System.ComponentModel.DataAnnotations;

namespace AlexParallelismApp.DAL.Models;

public class YEntity
{
    [Key] public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsLocked { get; set; }
}