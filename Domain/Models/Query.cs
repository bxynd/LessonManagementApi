using Domain.Enums;

namespace Domain.Models;

public static class Query
{
    public static string Sql { get; private set; }
    public static string SplitOn { get; private set; }
    private static string DateFilter { get; set; }
    private static string TeacherIdsFilter { get; set; }
    private static string StatusFilter { get; set; }
    private static string StudentsCountFilter { get; set; }
    public static void GetAllLessonsQuery(QueryParameters? queryParameters)
    {
        DateFilter = DateFilterParser(queryParameters.Date);
        StatusFilter = StatusFilterParser(queryParameters.Status);
        StudentsCountFilter = StudentsCountFilterParser(queryParameters.studentsCount);
        TeacherIdsFilter = TeacherIdsFilterParser(queryParameters.TeacherIds);

        var filtersList = new List<string>
        {
            DateFilter,
            StatusFilter,
            StudentsCountFilter,
            TeacherIdsFilter
        };
        
        var combinedFilters = CombineFilters(filtersList);

        Console.WriteLine(combinedFilters);
        Sql = @$"select latt.id,date,title,status,visitCount,visit,ls.student_id as sid,s.name as sname,tid,tname
                 from
                 ((select l.id,l.date,l.title,l.status,count(ls.visit) as visitCount
                 from lessons l
                 left join lesson_students ls on ls.lesson_id = l.id
                 left join students s on s.id = ls.student_id
                 where ls.visit = true
                 group by l.id)
                 union
                 (
                   select l.id,l.date,l.title,l.status,'0' as visitCount
                   from lessons l
                   where l.id not in 
                     (
                     select l.id
                     from lessons l
                     left join lesson_students ls on ls.lesson_id = l.id
                     left join students s on s.id = ls.student_id
                     where ls.visit = true
                     group by l.id
                     )
                 ))
                 as latt 
                 left join lesson_students as ls
                 left join students s on s.id = ls.student_id
                 on latt.id = ls.lesson_id
                 left join
                 (select l.id,t.id as tid,t.name as tname
                 from lessons l 
                 left join lesson_teachers ts on ts.lesson_id = l.id
                 left join teachers t on t.id = ts.teacher_id
                 ) as lt
                 on latt.id = lt.id 
                 {combinedFilters}
                 order by id
                 ";
        //{combinedFilters}
        SplitOn = "sid,tid"; 
    }
    
    private static string DateFilterParser(string? date)
    {
        if (date == null)
        {
            return "";
        }
        if (date.Contains(','))
        {
            var dates = date.Split(',');
            return @$" date > '{dates[0]}' and date < '{dates[1]}'";
        }
        return @$" date = '{date}'";
    }
    private static string StatusFilterParser(Status? status)
    {
        if (status == null)
        {
            return "";
        }
        return @$" latt.status = {(int)status}";
    }
    private static string StudentsCountFilterParser(string? studentsCount)
    {
        if (studentsCount == null)
        {
            return "";
        }
        if (studentsCount.Contains(','))
        {
            var count = studentsCount.Split(',');
            return @$" latt.visitCount >= '{count[0]}' and latt.visitCount <= '{count[1]}'";
        }
        return @$" latt.visitCount = '{studentsCount}'";;
    }
    private static string TeacherIdsFilterParser(string? teacherIds)
    {
        
        var queryPart = "";
        
        if (teacherIds == null)
        {
            return queryPart;
        }
        if (teacherIds.Contains(','))
        {
            var ids = teacherIds.Split(',');
            foreach (var id in ids)
            {
                queryPart = queryPart + @$" lt.id = '{id}'";
                if (id == ids.Last())
                {
                    break;
                }

                queryPart = queryPart + " or ";
            }
            return queryPart;
        }
        return @$" lt.id = '{teacherIds}'";
    }
    
    private static string CombineFilters(List<string> filters)
    {
        var querySnippet = "";

        foreach (var filter in filters)
        {
            if (filter != "")
            {
                querySnippet = "where ";
                break;
            }
        }

        foreach (var filter in filters)
        {
            if (filter != "" && querySnippet == "where ")
            {
                querySnippet = querySnippet + filter;
                continue;
            }
            if (filter != "" && querySnippet.Contains("where "))
            {
                querySnippet = querySnippet +" and "+ filter;
            }
        }
        return querySnippet;
    }
}