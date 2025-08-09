using Microsoft.AspNetCore.Mvc;
using StudentGradingApp.Data;
using Microsoft.EntityFrameworkCore;
using StudentGradingApp.Models;

namespace StudentGradingApp.Controller;

[ApiController]
[Route("api/teachers")]
public class TeacherController : ControllerBase
{
    private readonly AppDbContext _context;
    public TeacherController(AppDbContext context) => _context = context;
    
    [HttpGet("by-matiere")]
    public IActionResult GetTeachersGroupedByMatiere()
    {
        var result = _context.Matieres
            .Select(m => new
            {
                matiereId = m.Id,
                matiereNom = m.Nom,
                teachers = m.TeacherMatieres
                    .Select(tm => new {
                        tm.Teacher.Id,
                        tm.Teacher.FirstName,
                        tm.Teacher.LastName,
                        tm.Teacher.Gender
                    })
                    .ToList()
            })
            .ToList();

        return Ok(result);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var teachers = _context.Teachers
            .Include(c => c.ClassePrincipale)
            .ToList();
        return Ok(teachers);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var teacher = _context.Teachers
            .Include(t => t.ClassePrincipale)
            .Include(t => t.TeacherMatieres)
                .ThenInclude(tm => tm.Matiere)
            .FirstOrDefault(t => t.Id == id);
        if (teacher == null) return NotFound();
        return Ok(teacher);
    }

    [HttpPost]
    public IActionResult Add(Teacher teacher)
    {
        try
        {
            _context.Add(teacher);
            _context.SaveChanges();
            return Ok(new { message = "Professeur ajouté avec succès", teacher });
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(500, $"Erreur lors que l'ajout {ex.Message}.");
        }
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Teacher teacher)
    {
        if (id != teacher.Id) return BadRequest("ID du prof ne correspond pas à l'URL");

        var teacherToUpdate = _context.Teachers.Find(id);

        if (teacherToUpdate == null)
        {
            return NotFound("Ce prof est introuvable.");
        }
        try
        {      // _context.Entry(teacher).State = EntityState.Modified;
            _context.Entry(teacherToUpdate).CurrentValues.SetValues(teacher);
            _context.SaveChanges();
            return Ok($"Les informations du professeur {teacher.FirstName} {teacher.LastName} ont été mises à jour avec succès");
        }
        catch (DbUpdateConcurrencyException) //exception Currency modification in a same time
        {
            return Conflict("Modification en cours détectée.");
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var teacherToRemove = _context.Teachers.Find(id);

        if (teacherToRemove == null) return NotFound($"Le professeur avec ID : {id} est introuvable.");
        try
        {
            _context.Teachers.Remove(teacherToRemove);
            _context.SaveChanges();
            return Ok($"Le professeur : {teacherToRemove.FirstName} {teacherToRemove.LastName} a été supprimé.");
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(500, $"Erreur de suppression : {ex.Message}");
        }
    }
}