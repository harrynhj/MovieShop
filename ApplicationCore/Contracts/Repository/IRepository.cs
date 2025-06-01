namespace ApplicationCore.Contracts.Repository;

public interface IRepository<T> where T : class
{
    T Insert(T entity);
    T DeleteById(int id);
    T Update(T entity);
    IEnumerable<T> GetAll();
    T GetById(int id);
}