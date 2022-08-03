namespace Domain.Models;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }

    public List<Lesson> Lessons { get; set; }

    public Student()
    {
        Lessons = new List<Lesson>();
    }
}