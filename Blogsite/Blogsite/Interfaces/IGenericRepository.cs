namespace Blogsite.Interfaces
{
    public interface IGenericRepository<T> where T : class
        {
            IQueryable<T> GetAllAsQueryable();

            Task<List<T>> GetAll();

            Task<T> GetById(int id);

            Task Add(T entity);

            void Update(T entity);

            void Remove(T entity);

            Task<bool> SaveChanges();

            Task SaveChangesAsync();
        }
    }
