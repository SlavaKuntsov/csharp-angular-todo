using UserStore.Core.Abstractions;
using UserStore.Core.Models;

namespace UserStore.Application.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUserRepository _userRepository;

        public UsersService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _userRepository.Get();
        }

        public async Task<string> CreateUser(User user)
        {
            return await _userRepository.Create(user);
        }

        public string LoginUser(User user)
        {
            return _userRepository.Login(user);
        }

        public Object FindById(Guid id)
        {
            return _userRepository.FindById(id);
        }
        public bool FindExistingUser(string email)
        {
            return _userRepository.FindExistingUser(email);
        }

        public async Task<Guid> UpdateUser(Guid id, string email, string password)
        {
            return await _userRepository.Update(id, email, password);
        }

        public async Task<Guid> DeleteUser(Guid id)
        {
            return await _userRepository.Delete(id);
        }


    }
}
