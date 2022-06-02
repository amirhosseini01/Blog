namespace Site.ViewModels;

public class VmBlogList
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string CategoryTitle { get; set; }
    public int CategoryId { get; set; }
    public bool IsHidden { get; set; }
    public DateTime CreateDate { get; set; }
    public string PersianCreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public string PersianUpdateDate { get; set; }
}