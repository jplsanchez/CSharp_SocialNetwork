using User.Domain.Interfaces.Repositories;
using User.Domain.Models;

namespace User.Repository.Repositories
{
    public class UserRepository : IWriteRepository<UserModel>
    {
        private static readonly Dictionary<Guid, UserModel> Users = new();

        public async Task<IEnumerable<UserModel>> GetAll()
        {
            return await Task.Run(() => Users.Values.ToList());
        }

        public async Task<UserModel> Get(Guid id)
        {
            var user = await Task.Run(() => Users.GetValueOrDefault(id));
            return user ?? new UserModel();
        }

        public async Task Add(UserModel User)
        {
            await Task.Run(() => Users.Add(User.Id, User));
        }

        public async Task Edit(UserModel User)
        {
            await Task.Run(() =>
            {
                Users.Remove(User.Id);
                Users.Add(User.Id, User);
            });
        }

        public async Task Delete(Guid id)
        {
            await Task.Run(() => Users.Remove(id));
        }
    }
}