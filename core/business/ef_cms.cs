using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using qodev_content_management_services.db;
using qodev_content_management_services.implementation;
using qodev_content_management_services.models;
using qodev_content_management_services.repository;
using qodev_content_management_services.utils;
using qodev_content_management_services.utils.classes;
using qodev_utilization.utils.Response;

namespace qodev_content_management_services.core.business;

public abstract class ef_cms<TEntity, TContext> : ICmsRepository<TEntity>
    where TEntity : class, ICms
    where TContext : DatabaseContext
{
    private readonly TContext _context;

    public ef_cms(TContext context)
    {
        this._context = context;
    }

    public async Task<AppResponse> enrollCms(Cms cms)
    {
        var findCms = await _context.Set<TEntity>().AnyAsync(x => x.pageKey == cms.pageKey || x.path == cms.path);
        if (findCms)
        {
            return new AppResponse { Success = false, ErrorMessage = "ContentKey already exists"};
        }
        else
        {
            await _context.CmsEnumerable.AddAsync(cms);
            await _context.SaveChangesAsync();
            return new AppResponse { Success = true, ErrorMessage = "Success"};
        }
    }

    public async Task<List<TEntity>> CmsList(CmsChangeScreenParams cmsChangeScreenParams)
    {
        List<TEntity> cms = await _context.Set<TEntity>()
            .Where(x => x.path == cmsChangeScreenParams.currentKey).ToListAsync();
        return cms;
    }

    public async Task<dynamic> CmsUrlKey(CmsChangeScreenParams cmsChangeScreenParams)
    {
        var findUrlKey = await _context.Set<TEntity>()
            .Where(x => x.path == cmsChangeScreenParams.currentKey)
            .FirstOrDefaultAsync();
        if (findUrlKey != null)
        {
            return findUrlKey.path;
        }
        else
        {
            return null;
        }
    }


   
    public async Task<dynamic> CheckCmsPaths(CmsChangeScreenParams cmsChangeScreenParams)
    {
        var findAnyCmsPaths = await _context.Set<TEntity>()
            .AnyAsync(x => x.path == cmsChangeScreenParams.currentKey);
        return findAnyCmsPaths;
    }

    public async Task<AppResponse> CmsInit()
    {
        string projectRoot = AppDomain.CurrentDomain.BaseDirectory;
        string jsonFilePath = Path.Combine(projectRoot, "utils", "storage", "cms.json");
        var json = File.ReadAllText(jsonFilePath);
        var cmsJson = JsonConvert.DeserializeObject<Root>(json);

        foreach (var VARIABLE in cmsJson.cmsdeserialize)
        {
            var checkCmsIfExist = await _context.Set<TEntity>()
                .AnyAsync(x => x.pageKey == VARIABLE.pageKey);
            if (checkCmsIfExist)
            {
                return new AppResponse { Success = false, ErrorMessage = "cms_exist" };
            }
            else
            {
                var entity = new Cms
                {
                    pageKey = VARIABLE.pageKey,
                    access = VARIABLE.access,
                    path = VARIABLE.path,
                    isDisabled = VARIABLE.isDisabled,
                    content = JsonConvert.SerializeObject(VARIABLE.content),
                    created_at = DateTime.Now,
                    updated_at = DateTime.Now
                };
                await _context.CmsEnumerable.AddAsync(entity);
                await _context.SaveChangesAsync();
                
            }
        }

        return new AppResponse { Success = false, ErrorMessage = "no_cms" };
    }
}