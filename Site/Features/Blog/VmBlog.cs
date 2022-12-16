using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Site.Features.Blog;

public class VmBlogList
{
    public int Id { get; set; }
    public int? OrderView { get; set; }
    public string Title { get; set; }
    public string CategoryTitle { get; set; }
    public int CategoryId { get; set; }
    public bool IsHidden { get; set; }
    public DateTime CreateDate { get; set; }
    public string PersianCreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public string PersianUpdateDate { get; set; }
}

public class VmBlogDetail
{
    [DisplayName("کد")]
    public int? Id { get; set; }

    [DisplayName("دسته بندی")]
    [Required(ErrorMessage = "{0} را انتخاب کنید.")]
    [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "{0} را انتخاب کنید.")]
    public int CategoryId { get; set; }

    [DisplayName("عنوان")]
    [Required(ErrorMessage = "{0} را وارد کنید.")]
    [StringLength(maximumLength: 60, MinimumLength = 2, ErrorMessage = "{0} باید بین {2} تا {1} کاراکتر باشد.")]
    public string Title { get; set; }

    [DisplayName("توضیحات متا")]
    [Required(ErrorMessage = "{0} را وارد کنید.")]
    [StringLength(maximumLength: 160, MinimumLength = 2, ErrorMessage = "{0} باید بین {2} تا {1} کاراکتر باشد.")]
    public string MetaDescription { get; set; }

    [DisplayName("توضیحات")]
    [Required(ErrorMessage = "{0} را وارد کنید.")]
    public string Description { get; set; }

    [DisplayName("عدم انتشار")]
    public bool IsHidden { get; set; }

    [DisplayName("تصویر")]
    public string ImgBase64 { get; set; }

    [DisplayName("تصویر")]
    public string ImgUrl { get; set; }

    [DisplayName("ALT تصویر")]
    [Required(ErrorMessage = "{0} را وارد کنید.")]
    [StringLength(maximumLength: 60, MinimumLength = 2, ErrorMessage = "{0} باید بین {2} تا {1} کاراکتر باشد.")]
    public string ImgAlt { get; set; }

    [DisplayName("توضیحات تصویر")]
    [Required(ErrorMessage = "{0} را وارد کنید.")]
    [StringLength(maximumLength: 60, MinimumLength = 2, ErrorMessage = "{0} باید بین {2} تا {1} کاراکتر باشد.")]
    public string ImgTitle { get; set; }

    [DisplayName("لینک مقاله اصلی")]
    [StringLength(maximumLength: 450, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد.")]
    public string CanonicalUrl { get; set; }

    [DisplayName("کلمات کلیدی")]
    [Required(ErrorMessage = "{0} را وارد کنید.")]
    [StringLength(maximumLength: 500, MinimumLength = 2, ErrorMessage = "{0} باید بین {2} تا {1} کاراکتر باشد.")]
    public string KeyWords { get; set; }
}