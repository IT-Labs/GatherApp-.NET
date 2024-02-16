using GatherApp.Contracts.Entities.Interfaces;
using GatherApp.DataContext;

namespace GatherApp.Repositories.Impl
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity<string>
    {
        protected readonly GatherAppContext _gatherAppContext;

        public Repository(GatherAppContext gatherAppContext)
        {
            _gatherAppContext = gatherAppContext;
        }

        public void Add(TEntity entity)
        {
            _gatherAppContext.Set<TEntity>().Add(entity);
        }

        public TEntity Get(string id)
        {
            return _gatherAppContext.Set<TEntity>().FirstOrDefault(e => e.Id == id);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _gatherAppContext.Set<TEntity>();
        }

        public void Delete(TEntity entity)
        {
            _gatherAppContext.Set<TEntity>().Update(entity);
        }

        public void HardDelete(TEntity entity)
        {
            _gatherAppContext.Remove(entity);
        }

        public void Update(TEntity entity)
        {
            _gatherAppContext.Update(entity);
        }

    }
}
