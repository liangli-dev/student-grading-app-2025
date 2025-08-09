using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentGradingApp.Data;

[ApiController]
[Route("api/classes")]
public class ClassesController : ControllerBase
{
    private readonly AppDbContext _context;

    public ClassesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAllClasses()
    {
        var classes = _context.Classes
            .Select(c => new
            {
                c.Id,
                c.Niveau
            })
        .ToList();
        return Ok(classes);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var classe = _context.Classes
            .Include(c => c.TeacherPrincipal)
            .Include(c => c.Students)
            .FirstOrDefault(c => c.Id == id);

        if (classe == null) return NotFound();
        return Ok(classe);
    }

    [HttpGet("{id}/principal-teacher")]
    public IActionResult GetPrincipalTeacher(int id)
    {
        var teacher = _context.Classes
            .Include(c => c.TeacherPrincipal)
            .Where(c => c.Id == id)
            .Select(c => c.TeacherPrincipal)
            .FirstOrDefault();

        return teacher != null ? Ok(teacher) : NotFound();
    }
}