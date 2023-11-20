using AlexParallelismApp.DAL.Interfaces;
using AlexParallelismApp.DAL.Models;
using AlexParallelismApp.Domain.Interfaces.YEntity;
using AlexParallelismApp.Domain.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace AlexParallelismApp.Domain.Providers;

public class YEntitiesProvider : IYEntitiesProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IYEntityRepository _yEntityRepository;
    private readonly IMapper _mapper;

    public YEntitiesProvider(IYEntityRepository yEntityRepository, IMapper mapper,
        IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        _yEntityRepository = yEntityRepository;
        _mapper = mapper;
    }

    private HttpContext Context => _httpContextAccessor.HttpContext;

    public async Task<IResult<List<YEntityDto>>> GetYEntitiesAsync()
    {
        var yEntitiesList = await _yEntityRepository.GetAllAsync();
        var listDto = _mapper.Map<List<YEntityDto>>(yEntitiesList);
        return ResultCreator.GetValidResult(listDto);
    }

    public async Task<IResult<YEntityDto>> GetYEntityAsync(int id)
    {
        YEntity yEntity = await _yEntityRepository.FindAsync(id);
        if (!yEntity.IsLocked)
        {
            await _yEntityRepository.LockAsync(id, Context.Session.Id);
            if (_yEntityRepository.FindAsync(id).Result.Id == 0)
            {
                return ResultCreator.GetInvalidResult<YEntityDto>(
                    Constants.ErrorMessages.ObjectDeleted, ErrorStatus.ObjectDeleted);
            }

            YEntityDto xEntityDto = _mapper.Map<YEntityDto>(yEntity);
            return ResultCreator.GetValidResult(xEntityDto);
        }

        return ResultCreator.GetInvalidResult<YEntityDto>(string.Format(
                Constants.ErrorMessages.PessimisticVersionConflict, yEntity.SessionId),
            ErrorStatus.ObjectUpdated);
    }
}