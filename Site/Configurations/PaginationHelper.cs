using Site.ViewModels;

namespace Site.Configurations;
public static class PaginationHelper
{
    public static IQueryable<T> GetPagiantionQuery<T>(this IQueryable<T> query, VmRequestPagination vm)
    {
        return query.Skip((vm.Skip - 1) * vm.Take).Take(vm.Take);
    }
}