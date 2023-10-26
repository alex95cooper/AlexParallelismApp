using AlexParallelismApp.Domain.Models;

namespace AlexParallelismApp.Domain.Interfaces.XEntity;

public interface IXEntitiesUpdater
{
    Task<IResult> UpdateXEntityAsync(XEntityDto xEntityDto);

    Task<IResult> DeleteXEntityAsync(XEntityDto xEntityDto);
}