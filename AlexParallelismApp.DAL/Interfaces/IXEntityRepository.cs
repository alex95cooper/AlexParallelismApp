using AlexParallelismApp.DAL.Models;

namespace AlexParallelismApp.DAL.Interfaces;

public interface IXEntityRepository
{
    Task<XEntity> FindAsync(int id);

    Task<List<XEntity>> GetAllAsync();

    Task CreateAsync(XEntity entity);

    Task UpdateAsync(XEntity entity);

    Task DeleteAsync(XEntity entity);
}