using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Site.Features.Menu;

public class VmMenuList
{
    public int Id { get; set; }
    public int? OrderView { get; set; }
    public string Title { get; set; }
    public string Url { get; set; }
    public bool IsHidden { get; set; }
    public DateTime CreateDate { get; set; }
    public string PersianCreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public string PersianUpdateDate { get; set; }
}