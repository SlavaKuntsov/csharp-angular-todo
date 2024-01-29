using CSharpFunctionalExtensions;
using UserStore.Core.Models;

namespace UserStore.Core.Abstractions
{
    public interface IUsersService
    {
        Task<string> CreateUser(User user);
        Result<Object> LoginUser(User user);
        User FindById(Guid id);
        bool FindExistingUser(string email);
        Task<Guid> DeleteUser(Guid id);
        Task<List<User>> GetAllUsers();
        Task<Guid> UpdateUser(Guid id, string email, string password);
    }
}