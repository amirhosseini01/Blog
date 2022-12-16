using Site.ViewModels;

namespace Site.Repositories.Generics;
public interface IGenericRepository<T>
{
    Task<T> GetById(int id);
    Task<List<T>> GetAll();
    IQueryable<T> GetQuery();
    Task Add(T entity, string UpdateUserId);
    void Update(T entity, string UpdateUserId);
    void Remove(T entity);
    Task<ResponsePayload> Save();
}