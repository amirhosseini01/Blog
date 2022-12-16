using Site.ViewModels;

namespace Site.Configurations;
public static class ImgHelper
{
    private static readonly string[] validExtentions = { ".jpg", ".png", ".jpeg", ".webp", ".jpeg-2000", ".pdf", ".doc", ".3gp", ".docx", ".txt", ".xlsx", ".mp4", ".mkv", ".3gp" };
    public static async Task<ResponsePayload<string>> UploadFile(this IFormFile userfile, string fileName, FileSizeType fileSizeTypes)
    {
        string serverPath = Directory.GetCurrentDirectory() + "/wwwroot";
        var ext = Path.GetExtension(userfile.FileName);
        try
        {
            if (string.IsNullOrWhiteSpace(fileName) || userfile is null)
                return new ResponsePayload<string>(false, "فایل را وارد کنید.", null);
            if (!validExtentions.Contains(ext))
                return new ResponsePayload<string>(false, "این فایل مجاز به بارگذاری نمی باشد.", null);
            if (userfile.Length / 1024f >= fileSizeTypes.Size)
                return new ResponsePayload<string>(false, "حجم فایل ارسالی بیش از حد مجاز است.", null);

            string file = serverPath + fileName + ext;
            using (var stream = new FileStream(file, FileMode.Create))
            {
                await userfile.CopyToAsync(stream);
            }

            return new ResponsePayload<string>(true, "فایل با موفقیت آپلود شد.", fileName + ext);
        }
        catch (Exception ex)
        {
            //todo: log exception
            string exMessage = $"{ex.Message} {ex.InnerException?.Message}";
            return new ResponsePayload<string>(false, "خطا در آپلود فایل.", null);
        }
    }

    public static async Task<ResponsePayload<string>> SaveBase64(this string imgBase64, string filePath, FileSizeType fileSizeType)
    {
        if (string.IsNullOrWhiteSpace(imgBase64))
            return new ResponsePayload<string>(false, "فایل را وارد کنید.", null);

        string data;
        if (imgBase64.StartsWith("data:"))
        {
            string[] base64Arr = imgBase64.Split(',');
            if (base64Arr.Length == 0)
                return new ResponsePayload<string>(false, "فایل را وارد کنید.", null);
            data = base64Arr[1];
        }
        else
        {
            data = imgBase64;
        }

        byte[] bytes = Convert.FromBase64String(data);
        var fileType = GetFileExtension(imgBase64);
        if (string.IsNullOrEmpty(fileType))
            return new ResponsePayload<string>(false, "فایل وارد شده صحیح نمی باشد.", null);

        using var stream = new MemoryStream(bytes);
        IFormFile file = new FormFile(stream, 0, bytes.Length, filePath, "." + fileType);

        string fileName = Guid.NewGuid().ToString().Replace("-", "");
        return await UploadFile(file, filePath + fileName, fileSizeType);
    }
    public static void DeleteFile(this string fileUrl)
    {
        string serverPath = Directory.GetCurrentDirectory() + "/wwwroot";
        var file = serverPath + fileUrl;
        if (File.Exists(file))
            File.Delete(file);
    }
    private static string GetFileExtension(string base64String)
    {
        string data;

        if (base64String.StartsWith("data:"))
        {
            string[] base64Arr = base64String.Split(',');
            if (base64Arr.Length == 0)
                return "";
            data = base64Arr[1];
        }
        else
        {
            data = base64String;
        }
        return data.Substring(0, 5).ToUpper() switch
        {
            "IVBOR" => "png",
            "/9J/4" => "jpg",
            "AAAAF" => "mp4",
            "JVBER" => "pdf",
            "AAABA" => "ico",
            "UMFYI" => "rar",
            "E1XYD" => "rtf",
            "U1PKC" => "txt",
            "MQOWM" => "srt",
            "77U/M" => "srt",
            "UESDB" => "",
            "" => "docx",
            _ => string.Empty,
        };
    }
}

public class FileSizeType
{
    public int Size { get; set; }
}