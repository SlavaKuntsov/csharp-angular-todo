using CSharpFunctionalExtensions;
using UserStore.Core.Models;

namespace UserStore.Core.Abstractions
{
    public interface IUserRepository
    {
        Task<string> Create(User user);

        Result<Object> Login(User user);
        Object FindById(Guid id);
        bool FindExisting(string email);
        Task<List<User>> Get();
        Task<Guid> Update(Guid id, string email, string password);
        Task<Guid> Delete(Guid id);
    }
}