using System.ComponentModel.DataAnnotations;

namespace StudentGradingApp.Models;

public class Note
{
    public int Id { get; set; }

    [Range(0, 20, ErrorMessage = "La note doit être entre 0 et 20")]
    public double Valeur { get; set; }

    public int StudentId { get; set; }

    public Student? Student { get; set; }
    
    public int MatiereId { get; set; }

    public Matiere? Matiere { get; set; }
}