using System.Text.Json.Serialization;

namespace Site.Configurations.DataTableJs;
public class FiltersFromRequestDataTable
{
    [JsonPropertyName("length")]
    public string Length { get; set; }

    [JsonPropertyName("start")]
    public string Start { get; set; }

    [JsonPropertyName("sortColumn")]
    public string SortColumn { get; set; }

    [JsonPropertyName("sortColumnDirection")]
    public string SortColumnDirection { get; set; }

    [JsonPropertyName("sortColumnIndex")]
    public string SortColumnIndex { get; set; }

    [JsonPropertyName("draw")]
    public string Draw { get; set; }

    [JsonPropertyName("searchValue")]
    public string SearchValue { get; set; }

    [JsonPropertyName("pageSize")]
    public int PageSize { get; set; }

    [JsonPropertyName("skip")]
    public int Skip { get; set; }
}