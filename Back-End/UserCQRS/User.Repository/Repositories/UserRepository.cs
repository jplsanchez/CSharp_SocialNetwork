using User.Domain.Interfaces.Repositories;
using User.Domain.Models;

namespace User.Repository.Repositories
{
    public class UserRepository : IWriteRepository<UserModel>, IReadRepository<UserModel>
    {
        private static readonly Dictionary<Guid, UserModel> Users = new();

        public async Task<IEnumerable<UserModel>> GetAll(CancellationToken cancelToken)
        {
            return await Task.Run(() => Users.Values.ToList(), cancelToken);
        }

        public async Task<UserModel> Get(Guid id, CancellationToken cancelToken)
        {
            var user = await Task.Run(() => Users.GetValueOrDefault(id), cancelToken);
            return user ?? new UserModel();
        }

        public async Task Add(UserModel User, CancellationToken cancelToken)
        {
            await Task.Run(() => Users.Add(User.Id, User), cancelToken);
        }

        public async Task Edit(UserModel User, CancellationToken cancelToken)
        {
            await Task.Run(() =>
            {
                Users.Remove(User.Id);
                Users.Add(User.Id, User);
            }, cancelToken);
        }

        public async Task Delete(Guid id, CancellationToken cancelToken)
        {
            await Task.Run(() => Users.Remove(id), cancelToken);
        }
    }
}