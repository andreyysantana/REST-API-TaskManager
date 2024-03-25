using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Data;

namespace TaskManagerApi.Controllers;

public class TaskController : ControllerBase
{
    private readonly AppDbContext _context;

    public TaskController(AppDbContext context)
    {
        _context = context;
    }
}