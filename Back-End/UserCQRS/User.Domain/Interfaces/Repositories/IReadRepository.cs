namespace User.Domain.Interfaces.Repositories
{
    public interface IReadRepository<T>
    {
        Task<IEnumerable<T>> GetAll(CancellationToken cancelToken);

        Task<T> Get(Guid id, CancellationToken cancelToken);
    }
}