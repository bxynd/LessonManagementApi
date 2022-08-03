using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Models;

public class Lesson
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Title { get; set; }
    public Status Status { get; set; }
    
    public List<Teacher> Teachers { get; set; }
    public List<Student> Students { get; set; }

    public Lesson()
    {
        Students = new List<Student>();
        Teachers = new List<Teacher>();
    }
}