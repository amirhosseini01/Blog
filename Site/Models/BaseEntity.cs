using System.ComponentModel.DataAnnotations;

namespace Site.Models;
public class BaseEntity
{
    public int Id { get; set; }
    [Required]
    public string CreateUserId { get; set; }
    [Required]
    public string UpdateUserId { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
}