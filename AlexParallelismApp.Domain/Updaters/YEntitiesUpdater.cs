using AlexParallelismApp.DAL.Interfaces;
using AlexParallelismApp.DAL.Models;
using AlexParallelismApp.Domain.Interfaces.YEntity;
using AlexParallelismApp.Domain.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace AlexParallelismApp.Domain.Updaters;

public class YEntitiesUpdater : IYEntitiesUpdater
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IYEntityRepository _yEntityRepository;
    private readonly IMapper _mapper;

    public YEntitiesUpdater(IYEntityRepository yEntityRepository, IMapper mapper,
        IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        _yEntityRepository = yEntityRepository;
        _mapper = mapper;
    }

    private HttpContext Context => _httpContextAccessor.HttpContext;

    public async Task<IResult> UpdateYEntityAsync(YEntityDto yEntityDto)
    {
        YEntity yEntityDal = _mapper.Map<YEntity>(yEntityDto);
        if (!_yEntityRepository.FindAsync(yEntityDal.Id).Result.IsLocked)
        {
            await _yEntityRepository.LockAsync(yEntityDal.Id, Context.Session.Id);
            if (_yEntityRepository.FindAsync(yEntityDal.Id).Result.Id == 0)
            {
                return ResultCreator.GetInvalidResult(
                    Constants.ErrorMessages.ObjectDeleted, ErrorStatus.ObjectDeleted);
            }

            await _yEntityRepository.UpdateAsync(yEntityDal, Context.Session.Id);
            await _yEntityRepository.UnlockAsync(yEntityDal.Id);
            return ResultCreator.GetValidResult();
        }

        return ResultCreator.GetInvalidResult(string.Format(
                Constants.ErrorMessages.PessimisticVersionConflict, yEntityDal.Name),
            ErrorStatus.ObjectUpdated);
    }

    public async Task<IResult> DeleteYEntityAsync(YEntityDto yEntityDto)
    {
        YEntity yEntityDal = _mapper.Map<YEntity>(yEntityDto);
        if (!_yEntityRepository.FindAsync(yEntityDal.Id).Result.IsLocked)
        {
            await _yEntityRepository.DeleteAsync(yEntityDal);
            return ResultCreator.GetValidResult();
        }

        return ResultCreator.GetInvalidResult(string.Format(
                Constants.ErrorMessages.PessimisticVersionConflict, yEntityDal.Name),
            ErrorStatus.ObjectUpdated);
    }
}