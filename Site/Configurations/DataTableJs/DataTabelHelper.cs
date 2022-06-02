namespace Site.Configurations.DataTableJs;

public static class DataTabelHelper
{
    public static void GetDataFromRequest(this HttpRequest Request, out FiltersFromRequestDataTable filtersFromRequest)
    {
        filtersFromRequest = new()
        {
            Draw = Request.Form["draw"].FirstOrDefault(),
            Start = Request.Form["start"].FirstOrDefault(),
            Length = Request.Form["length"].FirstOrDefault(),
            SortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault(),
            SortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault(),
            SearchValue = Request.Form["search[value]"].FirstOrDefault(),
            SortColumnIndex = Request.Form["order[0][column]"].FirstOrDefault()
        };

        filtersFromRequest.PageSize = filtersFromRequest.Length != null ? Convert.ToInt32(filtersFromRequest.Length) : 0;
        filtersFromRequest.Skip = filtersFromRequest.Start != null ? Convert.ToInt32(filtersFromRequest.Start) : 0;

        filtersFromRequest.SearchValue = filtersFromRequest.SearchValue?.ToLower();
    }
    public static PaginationDataTableResult<T> ToDataTableJs<T>(this IEnumerable<T> source, FiltersFromRequestDataTable filtersFromRequest)
    {
        int recordsTotal = source.Count();
        CofingPaging(ref filtersFromRequest, recordsTotal);
        var result = new PaginationDataTableResult<T>()
        {
            Draw = filtersFromRequest.Draw,
            RecordsFiltered = recordsTotal,
            RecordsTotal = recordsTotal,
            Data = source.OrderByIndex(filtersFromRequest).Skip(filtersFromRequest.Skip).Take(filtersFromRequest.PageSize).ToList()
        };

        return result;
    }

    private static void CofingPaging(ref FiltersFromRequestDataTable filtersFromRequest, int recordsTotal)
    {
        if (filtersFromRequest.PageSize == -1)
        {
            filtersFromRequest.PageSize = recordsTotal;
            filtersFromRequest.Skip = 0;
        }
    }
    private static IEnumerable<T> OrderByIndex<T>(this IEnumerable<T> source, FiltersFromRequestDataTable filtersFromRequest)
    {
        var props = typeof(T).GetProperties();
        string propertyName = "";
        for (int i = 0; i < props.Length; i++)
        {
            if (i.ToString() == filtersFromRequest.SortColumnIndex)
                propertyName = props[i].Name;
        }

        System.Reflection.PropertyInfo propByName = typeof(T).GetProperty(propertyName);
        if (propByName is not null)
        {
            if (filtersFromRequest.SortColumnDirection == "desc")
                source = source.OrderByDescending(x => propByName.GetValue(x, null));
            else
                source = source.OrderBy(x => propByName.GetValue(x, null));
        }

        return source;
    }
}