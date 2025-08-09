using System.ComponentModel.DataAnnotations;

namespace StudentGradingApp.Models;

public class Matiere
{
    public int Id { get; set; }

    [Required]
    public string Nom { get; set; }
    public List<TeacherMatiere> TeacherMatieres { get; set; } = new();
    public List<Note> Notes { get; set; } = new();
}