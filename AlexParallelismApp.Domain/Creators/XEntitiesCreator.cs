using AlexParallelismApp.DAL.Interfaces;
using AlexParallelismApp.DAL.Models;
using AlexParallelismApp.Domain.Interfaces.XEntity;
using AlexParallelismApp.Domain.Models;
using AutoMapper;

namespace AlexParallelismApp.Domain.Creators;

public class XEntitiesCreator : IXEntitiesCreator
{
    private readonly IXEntityRepository _xEntityRepository;
    private readonly IMapper _mapper;

    public XEntitiesCreator(IXEntityRepository xEntityRepository, IMapper mapper)
    {
        _xEntityRepository = xEntityRepository;
        _mapper = mapper;
    }

    public async Task<IResult> AddXEntityAsync(XEntityDto xEntityDto)
    {
        XEntity xEntityDal = _mapper.Map<XEntity>(xEntityDto);
        await _xEntityRepository.CreateAsync(xEntityDal);
        return ResultCreator.GetValidResult();
    }
}