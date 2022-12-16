using System.Text.Json.Serialization;

namespace Site.Configurations.DataTableJs;

public class PaginationDataTableResult<T>
{
    [JsonPropertyName("draw")]
    public string Draw { get; set; }

    [JsonPropertyName("recordsTotal")]
    public int RecordsTotal { get; set; }

    [JsonPropertyName("recordsFiltered")]
    public int RecordsFiltered { get; set; }

    [JsonPropertyName("data")]
    public IEnumerable<T> Data { get; set; }
}