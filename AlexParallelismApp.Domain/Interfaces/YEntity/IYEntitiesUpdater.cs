using AlexParallelismApp.Domain.Models;

namespace AlexParallelismApp.Domain.Interfaces.YEntity;

public interface IYEntitiesUpdater
{
    Task<IResult> UpdateYEntityAsync(YEntityDto yEntityDto);
    
    Task<IResult> CancelLock(int id);

    Task<IResult> DeleteYEntityAsync(YEntityDto yEntityDto);
}