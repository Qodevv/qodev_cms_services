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

    [Route("cms-list/{contentKey}"), HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> CmsList([FromRoute] string contentKey)
    {
        var result = await _repository.CmsList(contentKey);
        return Ok(result);
    }

    [Route("cms-filter-url-key/{contentKey}"), HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> CmsFilter([FromRoute] string contentKey)
    {
        var result = await _repository.CmsUrlKey(contentKey);
        return Ok(result);
    }

    [Route("cms-current-screen"), HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> CmsCurrentScreen()
    {
        var result = await _repository.CmsCurrentScreen();
        return Ok(result);
    }

    [Route("cms-update-current-screen"), HttpPost, HttpPut]
    [AllowAnonymous]
    public async Task<IActionResult> CmsUpdateCurrentScreen([FromBody] CmsChangeScreenParams cmsChangeScreenParams)
    {
        var result = await _repository.UpdateCmsCurrentScreen(cmsChangeScreenParams);
        return Ok(result);
    }

    [Route("cms-look-cms-paths"), HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> FindCmsPaths([FromBody] CmsChangeScreenParams cmsChangeScreenParams)
    {
        var result = await _repository.CheckCmsPaths(cmsChangeScreenParams);
        return Ok(result);
    }

    [Route("cms-page-not-found"), HttpPut]
    [AllowAnonymous]
    public async Task<IActionResult> FindPageNotFound()
    {
        var result = await _repository.UpdateScreenForPageNotFound();
        return Ok(result);
    }
}