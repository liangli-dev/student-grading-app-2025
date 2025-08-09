using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentGradingApp.Data;
using StudentGradingApp.Models;

[ApiController]
[Route("api/matieres")]
public class MatieresController : ControllerBase
{
    private readonly AppDbContext _context;

    public MatieresController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAllMatieres()
    {
        var matieres = _context.Matieres
        .Include(m => m.TeacherMatieres)
        .ThenInclude(tm => tm.Teacher)
        .ToList();
        return Ok(matieres);
    }

    [HttpPost("assign-to-teacher")]
    public IActionResult AssignToTeacher([FromBody] TeacherMatiere assignment)
    {
        if (!_context.Teachers.Any(t => t.Id == assignment.TeacherId))
            return BadRequest("Teacher not found");
        
        if (!_context.Matieres.Any(m => m.Id == assignment.MatiereId))
            return BadRequest("Subject not found");

        _context.teacherMatieres.Add(assignment);
        _context.SaveChanges();
        return Ok(assignment);
    }

    [HttpDelete("unassign/{teacherId}/{matiereId}")]
    public IActionResult UnassignTeacherFromMatiere(int teacherId, int matiereId)
    {
        var relation = _context.teacherMatieres
        .FirstOrDefault(tm => tm.TeacherId == teacherId && tm.MatiereId == matiereId);

        if (relation == null) return NotFound();

        _context.teacherMatieres.Remove(relation);
        _context.SaveChanges();
        return Ok(new { message = "Professeur retiré de la matière avec succès." });
    }

    [HttpGet("by-teacher/{teacherId}")]
    public IActionResult GetByTeacher(int teacherId)
    {
        var subjects = _context.teacherMatieres
            .Where(tm => tm.TeacherId == teacherId)
            .Include(tm => tm.Matiere)
            .Select(tm => tm.Matiere)
            .ToList();

        return Ok(subjects);
    }
}