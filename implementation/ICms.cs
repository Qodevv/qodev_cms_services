namespace qodev_content_management_services.implementation;

public interface ICms
{
    public Guid id { get; set; }
    public string contentKey { get; set; }
    public string path { get; set; }
    public string content { get; set; }
    public int access { get; set; }
    public int currentScreen { get; set; }
    public int isDisabled { get; set; }
    public DateTime created_at { get; set; }
    public DateTime updated_at { get; set; }
}