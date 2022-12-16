using Microsoft.EntityFrameworkCore;
using Site.Models;
using Site.ViewModels;

namespace Site.Repositories.Generics;
public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly DbContext _context;
    private readonly DbSet<T> _dbSet;
    public GenericRepository(DbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }
    public async Task Add(T entity, string UserId)
    {
        entity.UpdateUserId = UserId;
        entity.CreateUserId = UserId;
        entity.CreateDate = DateTime.Now;
        entity.UpdateDate = DateTime.Now;
        await _dbSet.AddAsync(entity);
    }
    public void Update(T entity, string UpdateUserId)
    {
        entity.UpdateDate = DateTime.Now;
        entity.UpdateUserId = UpdateUserId;
        _dbSet.Update(entity);
    }
    public void Remove(T entity)
    {
        _dbSet.Remove(entity);
    }

    public async Task<T> GetById(int id)
    {
        return await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
    }

    public IQueryable<T> GetQuery()
    {
        return _dbSet;
    }

    public async Task<ResponsePayload> Save()
    {
        try
        {
            await _context.SaveChangesAsync();
            return new ResponsePayload(true, Messages.SuccessSaved);
        }
        catch (Exception ex)
        {
            //todo: log this error message
            string errMessage = $"{ex.Message} {ex.InnerException?.Message}";
            return new ResponsePayload(true, Messages.SuccessSaved);
        }
    }

    public async Task<List<T>> GetAll()
    {
        return await GetQuery().ToListAsync();
    }
}