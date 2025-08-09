using System.ComponentModel.DataAnnotations;

namespace StudentGradingApp.Models;

public class TeacherMatiere
{
    public int TeacherId { get; set; }

    public Teacher? Teacher { get; set; }
    public int MatiereId { get; set; }
    

    public Matiere? Matiere { get; set; }
}