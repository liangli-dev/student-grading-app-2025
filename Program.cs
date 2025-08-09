using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using StudentGradingApp.Data;
using StudentGradingApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data source = gradesystem.db"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVueApp", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowVueApp");
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();

    if (!context.Teachers.Any())
    {
        var prof1 = new Teacher { FirstName = "Claire", LastName = "Durand", Gender = "F" };
        var prof2 = new Teacher { FirstName = "Marc", LastName = "Dubois", Gender = "M" };
        var prof3 = new Teacher { FirstName = "Merlin", LastName = "Leroy", Gender = "F" };
        context.AddRange(prof1, prof2, prof3);

        var mat1 = new Matiere { Nom = "Mathématiques" };
        var mat2 = new Matiere { Nom = "Histoire" };
        var mat3 = new Matiere { Nom = "Physique" };

        context.AddRange(mat1, mat2, mat3);
        context.SaveChanges();

        var classe3 = new Classe { Niveau = "Troisième", TeacherPrincipalId = prof3.Id };
        var classe1 = new Classe { Niveau = "Cinquième", TeacherPrincipalId = prof1.Id };
        var classe2 = new Classe { Niveau = "Quatrième", TeacherPrincipalId = prof2.Id };

        context.AddRange(classe1 , classe2, classe3);
        context.SaveChanges();

        context.AddRange(
            new TeacherMatiere { Teacher = prof1, Matiere = mat1 },
            new TeacherMatiere { Teacher = prof1, Matiere = mat2 },
            new TeacherMatiere { Teacher = prof2, Matiere = mat2 },
            new TeacherMatiere { Teacher = prof3, Matiere = mat3 },
            new TeacherMatiere { Teacher = prof3, Matiere = mat1 }
        );

        var students = new[] {
        new Student
            {
                FirstName = "Luc",
                LastName = "Martin",
                Gender = "M",
                ClassId = classe1.Id,
                Notes = new List<Note>
                {
                    new Note { Matiere = mat1, Valeur = 18 },
                    new Note { Matiere = mat2, Valeur = 15 }
                }
            },
            new Student
            {
                FirstName = "Li",
                LastName = "Liang",
                Gender = "F",
                ClassId = classe2.Id,
                Notes = new List<Note>
                {
                    new Note { Matiere = mat1, Valeur = 11 },
                    new Note { Matiere = mat3, Valeur = 17 }
                }
            },
            new Student
            {
                FirstName = "Jing",
                LastName = "Wang",
                Gender = "M",
                ClassId = classe3.Id,
                Notes = new List<Note>
                {
                    new Note { Matiere = mat2, Valeur = 19 },
                    new Note { Matiere = mat3, Valeur = 10 }
                }
            }
        };
        context.AddRange(students);
        context.SaveChanges();

        Console.WriteLine($"Nb de prof créé: {context.Teachers.Count()}");
        Console.WriteLine($"Nb de classe créé: {context.Classes.Count()}");
    }
}

app.Run();

