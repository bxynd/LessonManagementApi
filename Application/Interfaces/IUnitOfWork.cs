namespace Application.Interfaces;

public interface IUnitOfWork
{
    ILessonRepository Lessons { get; }
}