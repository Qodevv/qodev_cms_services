using Microsoft.EntityFrameworkCore;
using qodev_content_management_services.db;
using qodev_content_management_services.implementation;
using qodev_content_management_services.models;
using qodev_content_management_services.repository;
using qodev_content_management_services.utils;
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
        var findCms = await _context.Set<TEntity>().AnyAsync(x => x.contentKey == cms.contentKey || x.path == cms.path);
        if (findCms)
        {
            return new AppResponse { Success = false, ErrorMessage = "ContentKey already exists"};
        }
        else
        {
            cms.created_at = DateTime.Now;
            cms.updated_at = DateTime.Now;
            await _context.CmsEnumerable.AddAsync(cms);
            await _context.SaveChangesAsync();
            return new AppResponse { Success = true, ErrorMessage = "Success"};
        }
    }

    public async Task<List<TEntity>> CmsList(string contentKey)
    {
        List<TEntity> cms = await _context.Set<TEntity>()
            .Where(x => x.contentKey == contentKey).ToListAsync();
        return cms;
    }

    public async Task<dynamic> CmsUrlKey(string contentKey)
    {
        var findUrlKey = await _context.Set<TEntity>()
            .Where(x => x.contentKey == contentKey)
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

    public async Task<dynamic> CmsCurrentScreen()
    {
        var getContentKeyByCurrentScreen = await _context
            .Set<TEntity>()
            .Where(x => x.currentScreen == 1)
            .FirstOrDefaultAsync();
        if (getContentKeyByCurrentScreen != null)
        {
            return getContentKeyByCurrentScreen.contentKey;
        }
        else
        {
            return "home-block";
        }
    }

    public async Task<dynamic> UpdateCmsCurrentScreen(CmsChangeScreenParams cmsChangeScreenParams)
    {
        var paths = new List<string> { cmsChangeScreenParams.currentKey };
        var itemToUpdate = await _context.Set<TEntity>()
            .Where(x => paths.Contains(x.path)).ToListAsync();
        if (itemToUpdate.Any())
        {
            foreach (var item in itemToUpdate)
            {
                item.currentScreen = (item.currentScreen == 0) ? 1 : 0;
            }

            await _context.SaveChangesAsync();
            return 200;
        }
        else
        {
            var item404 = await _context.Set<TEntity>().FirstOrDefaultAsync(item => item.path == "/page_not_found");
            if (item404 != null)
            {
                item404.currentScreen = 1;
                var allItems = await _context.Set<TEntity>().ToListAsync();

                foreach (var item in allItems)
                {
                    if (item.path != "/page_not_found")
                    {
                        item.currentScreen = 0;
                    }
                }
                await _context.SaveChangesAsync();
                return 404; 
            }
            else
            {
                return 500; 
            }
        }
    }

    public async Task<dynamic> CheckCmsPaths(CmsChangeScreenParams cmsChangeScreenParams)
    {
        var findAnyCmsPaths = await _context.Set<TEntity>()
            .AnyAsync(x => x.path == cmsChangeScreenParams.currentKey);
        return findAnyCmsPaths;
    }

    public async Task<dynamic> UpdateScreenForPageNotFound()
    {
        var item404 = await _context.Set<TEntity>().FirstOrDefaultAsync(item => item.path == "/page_not_found");
        if (item404 != null)
        {
            item404.currentScreen = 1;
            var allItems = await _context.Set<TEntity>().ToListAsync();

            foreach (var item in allItems)
            {
                if (item.path != "/page_not_found")
                {
                    item.currentScreen = 0;
                }
            }
            await _context.SaveChangesAsync();
            return 200; 
        }
        else
        {
            return 500; 
        }
    }
}