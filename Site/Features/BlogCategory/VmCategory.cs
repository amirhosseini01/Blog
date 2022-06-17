using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Site.Features.BlogCategory;

public class VmCategoryClientList
{
    public int Id { get; set; }
    public int? OrderView { get; set; }
    public int BlogCount { get; set; }
    public string Title { get; set; }
}

public class VmCategoryList
{
    public int Id { get; set; }
    public int? OrderView { get; set; }
    public string Title { get; set; }
    public bool IsHidden { get; set; }
    public DateTime CreateDate { get; set; }
    public string PersianCreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public string PersianUpdateDate { get; set; }
}

public class VmCategoryInput
{
    [DisplayName("کد")]
    public int? Id { get; set; }

    [DisplayName("عنوان")]
    [Required(ErrorMessage = "{0} را وارد کنید.")]
    [StringLength(maximumLength: 60, MinimumLength = 2, ErrorMessage = "{0} باید بین {2} تا {1} کاراکتر باشد.")]
    public string Title { get; set; }
}