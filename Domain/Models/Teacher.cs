namespace Domain.Models;

public class Teacher
{
    public int Tid { get; set; }
    public string Tname { get; set; }

    public List<Lesson> Lessons { get; set; }

    public Teacher()
    {
        Lessons = new List<Lesson>();
    }
}