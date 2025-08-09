using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentGradingApp.Models;

public class Student
{
    public int Id { get; set; }

    [Required]
    public required string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public string Gender { get; set; }

    [Required]
    public int ClassId { get; set; }

    [ForeignKey("ClassId")]
    public Classe? Classe { get; set; }
    public List<Note> Notes { get; set; } = new ();
}