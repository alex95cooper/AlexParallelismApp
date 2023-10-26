using AlexParallelismApp.Domain.Models;

namespace AlexParallelismApp.Domain.Interfaces.YEntity;

public interface IYEntitiesCreator
{
    Task<IResult> AddYEntityAsync(YEntityDto yEntityDto);
}