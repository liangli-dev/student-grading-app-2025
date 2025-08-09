using Microsoft.AspNetCore.Mvc;
using StudentGradingApp.Data;
using Microsoft.EntityFrameworkCore;
using StudentGradingApp.Models;

namespace StudentGradingApp.Controller;

[ApiController]
[Route("api/students")]
public class StudentController : ControllerBase
{
    private readonly AppDbContext _context;
    public StudentController(AppDbContext context) => _context = context;

    [HttpGet("by-class/{classId}")]
    public IActionResult GetByClass(int classId)
    {
        var students = _context.Students
            .Where(s => s.ClassId == classId)
            .ToList();
        return Ok(students);
    }

    [HttpPost]
    public IActionResult Add(Student student)
    {
        try
        {
            _context.Students.Add(student);
            _context.SaveChanges();
            return Ok(student);
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(500, $"Erreur lors que l'ajout {ex.Message}.");
        }
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Student student)
    {
        if (id != student.Id) return BadRequest("ID de l'élève ne correspond pas à l'URL");

        var studentToUpdate = _context.Students.Find(id);

        if (studentToUpdate == null)
        {
            return NotFound($"L'élève avec ID : {id} est introuvable.");
        }

        try
        {
            _context.Entry(studentToUpdate).CurrentValues.SetValues(student);
            _context.SaveChanges();
            return Ok($"Les informations de l'élève {student.FirstName} {student.LastName} ont été mises à jour avec succès");
        }
        catch (DbUpdateConcurrencyException)
        {
            return Conflict("Modification en cours détectée.");
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var studentToRemove = _context.Students.Find(id);

        if (studentToRemove == null)
        {
            return NotFound($"L'élève avec ID : {id} est introuvable.");
        }
        try
        {
            _context.Students.Remove(studentToRemove);
            _context.SaveChanges();
            return Ok($"L'élève : {studentToRemove.FirstName} {studentToRemove.LastName} a été supprimé.");
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(500, $"Erreur de suppression {ex.Message}");
        }
    }
}