using Application.Interfaces;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<ILessonRepository, LessonRepository>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();
    }
}