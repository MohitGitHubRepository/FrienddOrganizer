using System.Data.Entity;
using System.Threading.Tasks;

namespace FrienddOrganizer.UI.DataService.Repository
{
    public class RepositoryBase<TEntity, Tcontext> : IRepositoryBase<TEntity>
        where TEntity : class
        where Tcontext : DbContext
    {
        protected readonly Tcontext _context;

        protected RepositoryBase(Tcontext context)
        {
            _context = context;
        }

        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public virtual async Task<TEntity> getById(int? Id)
        {
            return await _context.Set<TEntity>().FindAsync(Id);
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }

        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
