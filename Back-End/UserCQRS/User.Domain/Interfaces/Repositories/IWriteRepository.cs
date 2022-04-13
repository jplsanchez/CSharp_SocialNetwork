namespace User.Domain.Interfaces.Repositories
{
    public interface IWriteRepository<T>
    {
        Task Add(T item, CancellationToken cancelToken);

        Task Edit(T item, CancellationToken cancelToken);

        Task Delete(Guid id, CancellationToken cancelToken);
    }
}