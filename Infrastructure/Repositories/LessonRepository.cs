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
    public async Task<IEnumerable<Lesson>> GetAllAsync()
    {
        var query = @"select l.id,l.date,l.title,l.status,s.id,s.name,ls.visit,t.id,t.name
                      from lessons l 
                      inner join lesson_students ls on ls.lesson_id = l.id
                      inner join students s on s.id = ls.student_id
                      inner join lesson_teachers lt on lt.lesson_id = l.id
                      inner join teachers t on t.id = lt.teacher_id";
        
        using var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        var lessons = await connection.QueryAsync<Lesson,Student,Teacher,Lesson>(query,
            (lesson,student,teacher) =>
            {
                if(student == null)
                {
                    return lesson;
                }
                if(teacher == null)
                {
                    return lesson;
                }
                lesson.Students.Add(student);
                lesson.Teachers.Add(teacher);
                return lesson;
            },
            splitOn: "id,id"
        );
        return lessons;
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