using AlexParallelismApp.Domain.Interfaces.YEntity;
using AlexParallelismApp.Domain.Models;
using AlexParallelismApp.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AlexParallelismApp.Controllers;

public class YEntityController : Controller
{
    private readonly IYEntitiesCreator _yEntitiesCreator;
    private readonly IYEntitiesProvider _yEntitiesProvider;
    private readonly IYEntitiesUpdater _yEntitiesUpdater;
    private readonly IMapper _mapper;
    private readonly CancelLockTimer _timer;

    public YEntityController(IYEntitiesCreator yEntitiesCreator, IYEntitiesProvider yEntitiesProvider,
        IYEntitiesUpdater yEntitiesUpdater, IMapper mapper)
    {
        _yEntitiesCreator = yEntitiesCreator;
        _yEntitiesProvider = yEntitiesProvider;
        _yEntitiesUpdater = yEntitiesUpdater;
        _mapper = mapper;
        _timer = new CancelLockTimer(this);
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var result = await _yEntitiesProvider.GetYEntitiesAsync();
        List<YEntityViewModel> listVm = _mapper.Map<List<YEntityViewModel>>(result.Data);
        return View(listVm);
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View(new YEntityViewModel());
    }

    [HttpGet]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _yEntitiesProvider.GetYEntityAsync(id);
        var model = _mapper.Map<YEntityViewModel>(result.Data);
        if (result.IsSuccess)
        {
            _timer.StartTimer(id);
            return View(model);
        }

        ViewBag.Message = result.Error; 
        return View("Notification");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(YEntityViewModel model)
    {
        YEntityDto entity = _mapper.Map<YEntityDto>(model);
        var result = await _yEntitiesCreator.AddYEntityAsync(entity);
        if (result.IsSuccess)
        {
            ViewBag.Message = Constants.Notifications.AddSuccessfully;
            return View("Notification");
        }

        ViewBag.Message = result.Error;
        return View("Notification");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(YEntityViewModel model)
    {
        YEntityDto entity = _mapper.Map<YEntityDto>(model);
        var result = await _yEntitiesUpdater.UpdateYEntityAsync(entity);
        if (result.IsSuccess)
        {
            ViewBag.Message = Constants.Notifications.UpdateSuccessfully;
            return View("Notification");
        }

        ViewBag.Message = result.Error;
        return View("Notification");
    }

    [HttpPost]
    public async Task CancelLock(int id)
    {
        await _yEntitiesUpdater.CancelLock(id);
        _timer.StopTimer(id);
    }
    
    [HttpPost]
    public void UpdateTimer(int id)
    {
        _timer.UpdateTimer(id);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        YEntityDto entity = new YEntityDto {Id = id};
        var deleteResult = await _yEntitiesUpdater.DeleteYEntityAsync(entity);
        if (deleteResult.IsSuccess)
        {
            ViewBag.Message = Constants.Notifications.DeleteSuccessfully;
            return View("Notification");
        }

        ViewBag.Message = deleteResult.Error;
        return View("Notification");
    }
}