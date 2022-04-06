namespace User.Domain.Interfaces.Repositories
{
    public interface IReadRepository<T>
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> Get(Guid id);
    }
}