using Application.Interfaces;
using Dapper;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Infrastructure.Repositories;

public class LessonRepository: ILessonRepository
{
    private readonly IConfiguration _configuration;

    public LessonRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public async Task<IEnumerable<Lesson>> GetAllAsync(QueryParameters? queryParameters)
    {
        Query.GetAllLessonsQuery(queryParameters);
        
        using var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        var lessons = await connection.QueryAsync<Lesson,Student,Teacher,Lesson>(Query.Sql,
            (lesson,student,teacher) =>
            {
                if(student == null)
                {
                    return lesson;
                }
                lesson.Students.Add(student);
                if(teacher == null)
                {
                    return lesson;
                }
                lesson.Teachers.Add(teacher);
                return lesson;
            },
            splitOn: Query.SplitOn
        );
        var result = lessons.GroupBy(l => l.Id).Select(g =>
        {
            var groupedLesson = g.First();
            groupedLesson.Students = g.Select(l => l.Students.SingleOrDefault()).Select(s=>s).Where(s=>s != null).DistinctBy(s=>s.Sid).ToList();
            groupedLesson.Teachers = g.Select(l => l.Teachers.SingleOrDefault()).Select(t=>t).Where(t=>t != null).DistinctBy(t=>t.Tid).ToList();
            return groupedLesson;
            
        }).OrderBy(l=>l.Id)
            .Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize)
            .Take(queryParameters.PageSize)
            .ToList();
        return result;
    }

    public Task<Lesson> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Guid> AddAsync(Lesson entity)
    {
        throw new NotImplementedException();
    }

    public Task<Guid> UpdateAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Guid> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}