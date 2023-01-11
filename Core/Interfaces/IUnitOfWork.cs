using Core.Entities;

namespace Core.Interfaces;

public interface IUnitOfWork: IDisposable
{
    IGenericRepository<TEntity> Repository<TEntity>();
    Task<int> Complete();
}