using Domain.Enums;

namespace Domain.Models;

public class QueryParameters
{
    public string? Date { get; set; }
    public Status? Status { get; set; }
    public string? TeacherIds { get; set; }
    public string? studentsCount { get; set; }
    
    private int maxPageSize = 15;
    public int PageNumber { get; set; } = 1;
    private int _pageSize = 5;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > maxPageSize) ? maxPageSize : value;
    }
}