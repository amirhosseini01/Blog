using Site.ViewModels;

namespace Site.Repositories.Generics;
public interface IGenericRepository<T>
{
    Task<T> GetById(int id);
    IQueryable<T> GetQuery();
    Task Add(T entity);
    void Update(T entity);
    void Remove(T entity);
    Task<ResponsePayload> Save();
}