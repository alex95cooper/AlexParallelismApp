using AlexParallelismApp.Domain.Models;

namespace AlexParallelismApp.Domain.Interfaces.YEntity;

public interface IYEntitiesProvider
{
    Task<IResult<List<YEntityDto>>> GetYEntitiesAsync();

    Task<IResult<YEntityDto>> GetYEntityAsync(int id);
}