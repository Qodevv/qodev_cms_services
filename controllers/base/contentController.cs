using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using qodev_authentication_services.auth;
using qodev_content_management_services.implementation;
using qodev_content_management_services.models;
using qodev_content_management_services.repository;
using qodev_content_management_services.utils;

namespace qodev_content_management_services.controllers;

[Route("api/v1/[controller]")]
[ApiController]
[ServiceFilter(typeof(KeyAuthFilter))]
public abstract class contentController<TEntity, TRepository> : ControllerBase
    where TEntity: class, ICms
    where TRepository: ICmsRepository<TEntity>
{
    private readonly TRepository _repository;

    public contentController(TRepository repository)
    {
        this._repository = repository;
    }

    [Route("enroll-cms"), HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> EnrollCms([FromBody] Cms cms)
    {
        var result = await _repository.enrollCms(cms);
        return Ok(result);
    }

    [Route("cms-list"), HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> CmsList([FromBody] CmsChangeScreenParams cmsChangeScreenParams)
    {
        var result = await _repository.CmsList(cmsChangeScreenParams);
        return Ok(result);
    }

    [Route("cms-filter-url-key"), HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> CmsFilter([FromBody] CmsChangeScreenParams cmsChangeScreenParams)
    {
        var result = await _repository.CmsUrlKey(cmsChangeScreenParams);
        return Ok(result);
    }


    [Route("cms-look-cms-paths"), HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> FindCmsPaths([FromBody] CmsChangeScreenParams cmsChangeScreenParams)
    {
        var result = await _repository.CheckCmsPaths(cmsChangeScreenParams);
        return Ok(result);
    }

    [Route("cms-initialization"), HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> CmsInitialization()
    {
        var result = await _repository.CmsInit();
        return Ok(result);
    }
}