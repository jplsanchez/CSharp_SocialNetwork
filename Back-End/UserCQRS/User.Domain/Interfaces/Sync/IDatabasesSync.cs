namespace User.Domain.Interfaces.Sync
{
    public interface IDatabasesSyncRepo<T>
    {
        Task Add(T item, CancellationToken cancelToken);

        Task Edit(T item, CancellationToken cancelToken);

        Task Delete(Guid id, CancellationToken cancelToken);
    }
}