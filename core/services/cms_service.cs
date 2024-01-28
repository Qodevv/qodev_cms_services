using qodev_content_management_services.core.business;
using qodev_content_management_services.db;
using qodev_content_management_services.models;

namespace qodev_content_management_services.core.constructor;

public class cms_service : ef_cms<Cms, DatabaseContext>
{
    public cms_service(DatabaseContext context) : base(context)
    {
    }
}
