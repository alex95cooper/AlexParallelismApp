using AlexParallelismApp.Domain.Models;

namespace AlexParallelismApp.Domain.Interfaces.XEntity;

public interface IXEntitiesCreator
{
    Task<IResult> AddXEntityAsync(XEntityDto xEntityDto);
}