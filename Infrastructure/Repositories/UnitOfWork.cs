using Application.Interfaces;

namespace Infrastructure.Repositories;

public class UnitOfWork:IUnitOfWork
{
    public UnitOfWork(ILessonRepository lessonRepository)
    {
        Lessons = lessonRepository;
    }
    public ILessonRepository Lessons { get; }
}