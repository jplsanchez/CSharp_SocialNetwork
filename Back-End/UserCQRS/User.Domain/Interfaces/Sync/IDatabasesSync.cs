namespace User.Domain.Interfaces.Sync
{
    public interface IDatabasesSync<T>
    {
        Task Add(T item, CancellationToken cancellationToken);

        Task Edit(T item, CancellationToken cancellationToken);

        Task Delete(Guid id, CancellationToken cancellationToken);
    }
}