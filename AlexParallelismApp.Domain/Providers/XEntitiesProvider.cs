using AlexParallelismApp.DAL.Interfaces;
using AlexParallelismApp.DAL.Models;
using AlexParallelismApp.Domain.Interfaces.XEntity;
using AlexParallelismApp.Domain.Models;
using AutoMapper;

namespace AlexParallelismApp.Domain.Providers;

public class XEntitiesProvider : IXEntitiesProvider
{
    private readonly IXEntityRepository _xEntityRepository;
    private readonly IMapper _mapper;

    public XEntitiesProvider(IXEntityRepository xEntityRepository, IMapper mapper)
    {
        _xEntityRepository = xEntityRepository;
        _mapper = mapper;
    }

    public async Task<IResult<List<XEntityDto>>> GetXEntitiesAsync()
    {
        var xEntitiesList = await _xEntityRepository.GetAllAsync();
        var listDto = _mapper.Map<List<XEntityDto>>(xEntitiesList);
        return ResultCreator.GetValidResult(listDto);
    }

    public async Task<IResult<XEntityDto>> GetXEntityAsync(int id)
    {
        XEntity xEntity = await _xEntityRepository.FindAsync(id);
        if (xEntity.Id == 0)
        {
            return ResultCreator.GetInvalidResult<XEntityDto>(
                Constants.ErrorMessages.ObjectDeleted, ErrorStatus.ObjectDeleted);
        }

        XEntityDto xEntityDto = _mapper.Map<XEntityDto>(xEntity);
        return ResultCreator.GetValidResult(xEntityDto);
    }
}