using UserStore.Core.Models;

namespace UserStore.Core.Abstractions
{
    public interface IUserRepository
    {
        Task<string> Create(User user);
        Task<List<User>> Get();
        Task<Guid> Update(Guid id, string email, string password);
        Task<Guid> Delete(Guid id);


        Object FindExisting(string email);
    }
}