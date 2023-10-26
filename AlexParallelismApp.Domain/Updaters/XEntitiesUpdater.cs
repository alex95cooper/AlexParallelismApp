using AlexParallelismApp.DAL.Interfaces;
using AlexParallelismApp.DAL.Models;
using AlexParallelismApp.Domain.Interfaces.XEntity;
using AlexParallelismApp.Domain.Models;
using AutoMapper;

namespace AlexParallelismApp.Domain.Updaters;

public class XEntitiesUpdater : IXEntitiesUpdater
{
    private readonly IXEntityRepository _xEntityRepository;
    private readonly IMapper _mapper;

    public XEntitiesUpdater(IXEntityRepository xEntityRepository, IMapper mapper)
    {
        _xEntityRepository = xEntityRepository;
        _mapper = mapper;
    }

    public async Task<IResult> UpdateXEntityAsync(XEntityDto xEntityDto)
    {
        XEntity xEntityDal = _mapper.Map<XEntity>(xEntityDto);
        XEntity dbXEntity = await _xEntityRepository.FindAsync(xEntityDal.Id);
        if (xEntityDal.UpdateTime == dbXEntity.UpdateTime)
        {
            await _xEntityRepository.UpdateAsync(xEntityDal);
            return ResultCreator.GetValidResult();
        }

        if (dbXEntity.UpdateTime == new DateTime())
        {
            return ResultCreator.GetInvalidResult(
                Constants.ErrorMessages.ObjectDeleted, ErrorStatus.ObjectDeleted);
        }

        return ResultCreator.GetInvalidResult(
            string.Format(Constants.ErrorMessages.OptimisticVersionConflict,
                xEntityDal.Name, xEntityDal.Description, xEntityDal.UpdateTime,
                dbXEntity.Name, dbXEntity.Description, dbXEntity.UpdateTime),
            ErrorStatus.ObjectUpdated);
    }

    public async Task<IResult> DeleteXEntityAsync(XEntityDto xEntityDto)
    {
        XEntity xEntityDal = _mapper.Map<XEntity>(xEntityDto);
        XEntity dbXEntity = await _xEntityRepository.FindAsync(xEntityDal.Id);
        if (xEntityDal.UpdateTime != dbXEntity.UpdateTime)
        {
            return ResultCreator.GetInvalidResult(
                string.Format(Constants.ErrorMessages.OptimisticVersionConflict,
                    xEntityDal.Name, xEntityDal.Description, xEntityDal.UpdateTime,
                    dbXEntity.Name, dbXEntity.Description, dbXEntity.UpdateTime),
                ErrorStatus.ObjectUpdated);
        }

        await _xEntityRepository.DeleteAsync(xEntityDal);
        return ResultCreator.GetValidResult();
    }
}