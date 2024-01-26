using UserStore.Core.Models;

namespace UserStore.Core.Abstractions
{
    public interface IUserRepository
    {
        Task<string> Create(User user);
        string Login(User user);
        Object FindById(Guid id);
        bool FindExistingUser(string email);
        Task<List<User>> Get();
        Task<Guid> Update(Guid id, string email, string password);
        Task<Guid> Delete(Guid id);
    }
}