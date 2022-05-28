namespace Site.Models;
public class BaseEntity
{
    public int Id { get; set; }
    public string CreateUserId { get; set; }
    public string UpdateUserId { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
}