using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Data;
using TaskManagerApi.Enum;
using TaskManagerApi.Models;

namespace TaskManagerApi.Controllers;

[ApiController]
[Route("[Controller]")]
public class TaskController : ControllerBase
{
    private readonly AppDbContext _context;

    
    public TaskController(AppDbContext context)
    {
        _context = context;
    }
    
    
    [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var task = _context.Tarefa.Find(id);
            if (task == null)
                return NotFound();

            
            return Ok(task);
        }
        

        [HttpGet("GetByTitle")]
        public IActionResult GetByTitle(string title)
        {
            var task = _context.Tarefa.Where(e => e.Title == title);
            if (task == null)
                return NotFound();

            
            return Ok(task);
        }
        

        [HttpGet("GetByDate")]
        public IActionResult GetByDate(DateTime date)
        {
            var task = _context.Tarefa.Where(x => x.DateTask.Date == date.Date);
            
            
            return Ok(task);
        }
        

        [HttpGet("GetByStatus")]
        public IActionResult GetByStatus(TaskStatusEnum status)
        {
            var task = _context.Tarefa.Where(e => e.Status == status);
            
            if (task == null)
                return NotFound();

            
            return Ok(task);
        }
        

        [HttpPut("{id}")]
        public IActionResult update(int id, TaskModel task)
        {
            var taskDb = _context.Tarefa.Find(id);

            if (taskDb == null)
                return NotFound();


            if (task.DateTask == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            taskDb.Description = task.Description;
            taskDb.Title = task.Title;
            taskDb.Status = task.Status;
            taskDb.DateTask = task.DateTask;

            _context.SaveChanges();

            
            return Ok();
        }
        

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var taskDb = _context.Tarefa.Find(id);

            if (taskDb == null)
                return NotFound();
            
            _context.Remove(taskDb);
            _context.SaveChanges();
            
            
            return NoContent();
        }
        

    [HttpGet("CatchById")]
    public IActionResult CatchById(int id)
    {
        var task = _context.Tarefa.Find(id);

        
        return Ok(task);
    }
    

    [HttpPost]
    public IActionResult CreateTask(TaskModel task)
    {
        if (task.DateTask == DateTime.MinValue)
            return BadRequest(new { Erro = "Task date cannot be empty" });

        _context.Add(task);
        _context.SaveChanges();
        
        
        return CreatedAtAction(nameof(CatchById), new { id = task.Id }, task);
    }
}