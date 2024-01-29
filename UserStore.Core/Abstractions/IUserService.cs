using CSharpFunctionalExtensions;
using UserStore.Core.Models;

namespace UserStore.Core.Abstractions
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers();
        Task<Result<string>> CreateUser(User user);
        Result<User> LoginUser(User user);
        Task<Guid> DeleteUser(Guid id);
        Task<Guid> UpdateUser(Guid id, string email, string password);


        //Result<User> FindExsitingUser(string email);
    }
}