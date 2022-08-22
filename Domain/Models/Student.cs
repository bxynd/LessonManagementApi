namespace Domain.Models;

public class Student
{
    public int Sid { get; set; }
    public string Sname { get; set; }
    public bool Visit { get; set; }
    public List<Lesson> Lessons { get; set; }

    public Student()
    {
        Lessons = new List<Lesson>();
    }
}