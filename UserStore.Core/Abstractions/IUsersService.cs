using UserStore.Core.Models;

namespace UserStore.Core.Abstractions
{
    public interface IUsersService
    {
        Task<string> CreateUser(User user);
        string LoginUser(User user);
        Object FindById(Guid id);
        bool FindExistingUser(string email);
        Task<Guid> DeleteUser(Guid id);
        Task<List<User>> GetAllUsers();
        Task<Guid> UpdateUser(Guid id, string email, string password);
    }
}