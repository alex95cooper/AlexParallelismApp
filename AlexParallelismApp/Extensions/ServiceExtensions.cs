using AlexParallelismApp.DAL.Interfaces;
using AlexParallelismApp.DAL.Repositories;
using AlexParallelismApp.Domain.Creators;
using AlexParallelismApp.Domain.Interfaces.XEntity;
using AlexParallelismApp.Domain.Interfaces.YEntity;
using AlexParallelismApp.Domain.Providers;
using AlexParallelismApp.Domain.Updaters;

namespace AlexParallelismApp.Extensions;

public static class ServiceExtensions
{
    public static void InitializeDataComponents(this IServiceCollection services, string connectionString)
    {
        services.AddTransient<IXEntityRepository, XEntityRepository>(_ =>
            new XEntityRepository(connectionString));
        services.AddTransient<IXEntitiesCreator, XEntitiesCreator>();
        services.AddTransient<IXEntitiesProvider, XEntitiesProvider>();
        services.AddTransient<IXEntitiesUpdater, XEntitiesUpdater>();
        services.AddTransient<IYEntityRepository, YEntityRepository>(_ =>
            new YEntityRepository(connectionString));
        services.AddTransient<IYEntitiesCreator, YEntitiesCreator>();
        services.AddTransient<IYEntitiesProvider, YEntitiesProvider>();
        services.AddTransient<IYEntitiesUpdater, YEntitiesUpdater>();
    }
}