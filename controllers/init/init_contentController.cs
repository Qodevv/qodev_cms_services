using Microsoft.AspNetCore.Mvc;
using qodev_content_management_services.core.constructor;
using qodev_content_management_services.models;

namespace qodev_content_management_services.controllers.init;

public class init_contentController : contentController<Cms, cms_service>
{
    public init_contentController(cms_service repository) : base(repository)
    {
    }
}