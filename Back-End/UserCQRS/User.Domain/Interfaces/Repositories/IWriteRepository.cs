namespace User.Domain.Interfaces.Repositories
{
    public interface IWriteRepository<T>
    {
        Task Add(T item);

        Task Edit(T item);

        Task Delete(Guid id);
    }
}