using Microsoft.AspNetCore.Mvc;
using StudentGradingApp.Data;
using Microsoft.EntityFrameworkCore;
using StudentGradingApp.Models;

namespace StudentGradingApp.Controller;

[ApiController]
[Route("api/notes")]
public class NoteController : ControllerBase
{
    private readonly AppDbContext _context;
    public NoteController(AppDbContext context) => _context = context;

    [HttpGet("by-student/{studentId}")]
    public IActionResult GetByStudent(int studentId)
    {
        var notes = _context.Notes
            .Include(n => n.Matiere)
                .ThenInclude(m => m.TeacherMatieres)
                .ThenInclude(tm => tm.Teacher)
            .Include(n => n.Student)
            .Where(n => n.StudentId == studentId)
            .ToList();
        return Ok(notes);
    }

    [HttpPost]
    public IActionResult Add(Note note)
    {
        if (note.Valeur < 0 || note.Valeur > 20)
            return BadRequest("Note doit être entre 0 et 20.");
        try
        {
            _context.Notes.Add(note);
            _context.SaveChanges();
            return Ok("Note est ajouté avec succes.");
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(500, $"Erreur lors que l'ajout {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Note note)
    {
        if (id != note.Id) return BadRequest("Cette note ne correspond pas à l'URL");

        var noteToUpdate = _context.Notes.Find(id);
        if (noteToUpdate == null)
            return NotFound("Ce note est introuvable.");
        try
        {
            _context.Entry(noteToUpdate).CurrentValues.SetValues(note);
            _context.SaveChanges();
            return Ok("Cette note a été bien mise à jour.");
        }
        catch (DbUpdateConcurrencyException)
        {
            return Conflict("Modification en cours détectée.");
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var noteToRemove = _context.Notes.Find(id);
        if (noteToRemove == null)
            return NotFound("Ce note est introuvable.");
        try
        { _context.Notes.Remove(noteToRemove);
            _context.SaveChanges();
            return Ok("Cette note a bien été supprimé."); }
        catch (DbUpdateException ex) {
            return StatusCode(500, $"Erreur de suppression : {ex.Message}");
        }
    }
}