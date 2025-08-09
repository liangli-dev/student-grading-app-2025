using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using StudentGradingApp.Models;

namespace StudentGradingApp.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Teacher> Teachers => Set<Teacher>();
    public DbSet<Classe> Classes => Set<Classe>();
    public DbSet<Matiere> Matieres => Set<Matiere>();
    public DbSet<Note> Notes => Set<Note>();
    public DbSet<TeacherMatiere> teacherMatieres => Set<TeacherMatiere>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Teacher>()
            .HasOne(t => t.ClassePrincipale)
            .WithOne(c => c.TeacherPrincipal)
            .HasForeignKey<Classe>(c => c.TeacherPrincipalId)
            .OnDelete(DeleteBehavior.SetNull);
            
        modelBuilder.Entity<TeacherMatiere>()
            .HasKey(tm => new { tm.TeacherId, tm.MatiereId });

        modelBuilder.Entity<TeacherMatiere>()
            .HasOne(tm => tm.Teacher)
            .WithMany(t => t.TeacherMatieres)
            .HasForeignKey(tm => tm.TeacherId);

        modelBuilder.Entity<TeacherMatiere>()
            .HasOne(tm => tm.Matiere)
            .WithMany(m => m.TeacherMatieres)
            .HasForeignKey(tm => tm.MatiereId);
    }

}