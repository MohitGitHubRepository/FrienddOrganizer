using System.Threading.Tasks;

namespace FrienddOrganizer.UI.DataService.Repository
{
    public interface IRepositoryBase<T>
    {
        Task<T> getById(int? Id);
        void Add(T entity);
        Task SaveAsync();
        void Remove(T entity);
        bool HasChanges();
         
    }
}