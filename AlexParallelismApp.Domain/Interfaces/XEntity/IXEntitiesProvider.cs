using AlexParallelismApp.Domain.Models;

namespace AlexParallelismApp.Domain.Interfaces.XEntity;

public interface IXEntitiesProvider
{
    Task<IResult<List<XEntityDto>>> GetXEntitiesAsync();

    Task<IResult<XEntityDto>> GetXEntityAsync(int id);
}