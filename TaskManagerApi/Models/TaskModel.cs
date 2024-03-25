using TaskManagerApi.Enum;

namespace TaskManagerApi.Models;

public class TaskModel
{
    public int Id { get; set; }
    public string Title  { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public TaskStatusEnum Status { get; set; }
}