using AlexParallelismApp.DAL.Interfaces;
using AlexParallelismApp.DAL.Models;
using AlexParallelismApp.Domain.Interfaces.YEntity;
using AlexParallelismApp.Domain.Models;
using AutoMapper;

namespace AlexParallelismApp.Domain.Creators;

public class YEntitiesCreator : IYEntitiesCreator
{
    private readonly IYEntityRepository _yEntityRepository;
    private readonly IMapper _mapper;

    public YEntitiesCreator(IYEntityRepository yEntityRepository, IMapper mapper)
    {
        _yEntityRepository = yEntityRepository;
        _mapper = mapper;
    }

    public async Task<IResult> AddYEntityAsync(YEntityDto yEntityDto)
    {
        YEntity yEntityDal = _mapper.Map<YEntity>(yEntityDto);
        await _yEntityRepository.CreateAsync(yEntityDal);
        return ResultCreator.GetValidResult();
    }
}