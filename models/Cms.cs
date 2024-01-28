using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using qodev_content_management_services.implementation;

namespace qodev_content_management_services.models;

[Table("cms")]
public class Cms : ICms
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid id { get; set; }
    [Required]
    public string contentKey { get; set; }

    public string path { get; set; }

    [Required]
    public string content { get; set; }
    public int access { get; set; }
    public int currentScreen { get; set; }
    public int isDisabled { get; set; }
    public DateTime created_at { get; set; }
    public DateTime updated_at { get; set; }
}