using AlexParallelismApp.Domain;
using AlexParallelismApp.Domain.Interfaces.XEntity;
using AlexParallelismApp.Domain.Models;
using AlexParallelismApp.Extensions;
using AlexParallelismApp.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AlexParallelismApp.Controllers;

public class XEntityController : Controller
{
    private readonly IXEntitiesCreator _xEntitiesCreator;
    private readonly IXEntitiesProvider _xEntitiesProvider;
    private readonly IXEntitiesUpdater _xEntitiesUpdater;
    private readonly IMapper _mapper;

    public XEntityController(IXEntitiesCreator ixEntitiesCreator, IXEntitiesProvider ixEntitiesProvider,
        IXEntitiesUpdater ixEntitiesUpdater, IMapper mapper)
    {
        _xEntitiesCreator = ixEntitiesCreator;
        _xEntitiesProvider = ixEntitiesProvider;
        _xEntitiesUpdater = ixEntitiesUpdater;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var result = await _xEntitiesProvider.GetXEntitiesAsync();
        List<XEntityViewModel> listVm = _mapper.Map<List<XEntityViewModel>>(result.Data);
        return View(listVm);
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View(new XEntityViewModel());
    }

    [HttpGet]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _xEntitiesProvider.GetXEntityAsync(id);
        var model = _mapper.Map<XEntityViewModel>(result.Data);
        if (result.IsSuccess)
        {
            return View(model);
        }

        ViewBag.Message = result.Error;
        return View("Notification");
    }

    [HttpGet]
    public IActionResult ErrorUpdate()
    {
        ViewBag.Message = TempData["Error"] as string;
        XEntityViewModel model = TempData.Get<XEntityViewModel>("Model");
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> ErrorDelete(int id)
    {
        var result = await _xEntitiesProvider.GetXEntityAsync(id);
        var model = _mapper.Map<XEntityViewModel>(result.Data);
        ViewBag.Message = TempData["Error"] as string;
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(XEntityViewModel model)
    {
        XEntityDto entity = _mapper.Map<XEntityDto>(model);
        var result = await _xEntitiesCreator.AddXEntityAsync(entity);
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
    public async Task<IActionResult> Update(XEntityViewModel model)
    {
        XEntityDto entity = _mapper.Map<XEntityDto>(model);
        var result = await _xEntitiesUpdater.UpdateXEntityAsync(entity);
        if (result.IsSuccess)
        {
            ViewBag.Message = Constants.Notifications.UpdateSuccessfully;
            return View("Notification");
        }

        if (result.ErrorStatus == ErrorStatus.ObjectUpdated)
        {
            TempData["Error"] = result.Error;
            TempData.Put("Model", model);
            return RedirectToAction("ErrorUpdate");
        }

        ViewBag.Message = result.Error;
        return View("Notification");
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id, long ticks)
    {
        DateTime date = new DateTime(ticks);
        XEntityDto entity = new XEntityDto {Id = id, UpdateTime = date};
        var deleteResult = await _xEntitiesUpdater.DeleteXEntityAsync(entity);
        if (deleteResult.IsSuccess)
        {
            ViewBag.Message = Constants.Notifications.DeleteSuccessfully;
            return View("Notification");
        }

        if (deleteResult.ErrorStatus == ErrorStatus.ObjectUpdated)
        {
            TempData["Error"] = deleteResult.Error;
            return RedirectToAction("ErrorDelete", new {id});
        }

        ViewBag.Message = deleteResult.Error;
        return View("Notification");
    }
}