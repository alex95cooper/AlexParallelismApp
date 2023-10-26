using AlexParallelismApp.DAL.Interfaces;
using AlexParallelismApp.DAL.Models;
using AlexParallelismApp.Domain.Interfaces.YEntity;
using AlexParallelismApp.Domain.Models;
using AutoMapper;

namespace AlexParallelismApp.Domain.Providers;

public class YEntitiesProvider : IYEntitiesProvider
{
    private readonly IYEntityRepository _yEntityRepository;
    private readonly IMapper _mapper;

    public YEntitiesProvider(IYEntityRepository yEntityRepository, IMapper mapper)
    {
        _yEntityRepository = yEntityRepository;
        _mapper = mapper;
    }

    public async Task<IResult<List<YEntityDto>>> GetYEntitiesAsync()
    {
        var yEntitiesList = await _yEntityRepository.GetAllAsync();
        var listDto = _mapper.Map<List<YEntityDto>>(yEntitiesList);
        return ResultCreator.GetValidResult(listDto);
    }

    public async Task<IResult<YEntityDto>> GetYEntityAsync(int id)
    {
        YEntity yEntity = await _yEntityRepository.FindAsync(id);
        if (yEntity.Id == 0)
        {
            return ResultCreator.GetInvalidResult<YEntityDto>(
                Constants.ErrorMessages.ObjectDeleted, ErrorStatus.ObjectDeleted);
        }

        YEntityDto xEntityDto = _mapper.Map<YEntityDto>(yEntity);
        return ResultCreator.GetValidResult(xEntityDto);
    }
}