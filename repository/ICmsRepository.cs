using qodev_content_management_services.implementation;
using qodev_content_management_services.models;
using qodev_content_management_services.utils;
using qodev_utilization.utils.Response;

namespace qodev_content_management_services.repository;

public interface ICmsRepository<T> where T : class, ICms
{
    public Task<AppResponse> enrollCms(Cms cms);
    public Task<List<T>> CmsList(string contentKey);
    public Task<dynamic> CmsUrlKey(string contentKey);
    public Task<dynamic> CmsCurrentScreen();
    public Task<dynamic> UpdateCmsCurrentScreen(CmsChangeScreenParams cmsChangeScreenParams);
    public Task<dynamic> CheckCmsPaths(CmsChangeScreenParams cmsChangeScreenParams);
    public Task<dynamic> UpdateScreenForPageNotFound();
}