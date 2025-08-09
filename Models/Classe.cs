using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentGradingApp.Models;

public class Classe
{
    public int Id { get; set; }

    [Required]
    public string Niveau { get; set; }
    public int? TeacherPrincipalId { get; set; }

    [ForeignKey(nameof(TeacherPrincipalId))]
    public Teacher? TeacherPrincipal { get; set; }
    public List<Student> Students { get; set; } = new();
}