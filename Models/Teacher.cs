using System.ComponentModel.DataAnnotations;

namespace StudentGradingApp.Models;

public class Teacher
{
    public int Id { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public string Gender { get; set; }
    
    public Classe? ClassePrincipale { get; set; }
    public List<TeacherMatiere> TeacherMatieres = new();
}