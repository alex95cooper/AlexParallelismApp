using AlexParallelismApp.DAL.Models;

namespace AlexParallelismApp.DAL.Interfaces;

public interface IYEntityRepository
{
    Task<YEntity> FindAsync(int id);

    Task<List<YEntity>> GetAllAsync();

    Task CreateAsync(YEntity entity);

    Task UpdateAsync(YEntity entity, string sessionId);

    Task LockAsync(int id, string sessionId);

    Task UnlockAsync(int id);

    Task DeleteAsync(YEntity entity);
}