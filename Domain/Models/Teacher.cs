namespace Domain.Models;

public class Teacher
{
    public int TeacherId { get; set; }
    public string Name { get; set; }

    public List<Lesson> Lessons { get; set; }

    public Teacher()
    {
        Lessons = new List<Lesson>();
    }
}