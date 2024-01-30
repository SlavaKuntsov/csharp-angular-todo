using CSharpFunctionalExtensions;
using UserStore.Core.Abstractions;
using UserStore.Core.Models;

namespace UserStore.Application.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _userRepository.Get();
        }

        public async Task<Result<string>> CreateUser(User user)
        {
            var existingUser = FindUnexsitingUser(user.Email);
            // if user not found - it is success

            if (existingUser.IsFailure)
            {
                return Result.Failure<string>(existingUser.Error);
            }

            return await _userRepository.Create(user);
        }

        public Result<User> LoginUser(User user)
        {
            var existingUser = FindExsitingUser(user.Email);

            // not found
            if (existingUser.IsFailure)
            {
                return Result.Failure<User>("User not found");
            }

            if (!BCrypt.Net.BCrypt.Verify(user.Password, existingUser.Value.Password))
            {
                return Result.Failure<User>("Wrong password");
            }

            if (!UserStore.Core.Models.User.ValidateToken(user.Token!, "9f6a1d7e5b3c8a4d9f6a1d7e5b3c8a4d"))
            {
                return Result.Failure<User>("Invalid token");
            }

            return Result.Success(existingUser.Value);
        }

        public async Task<Guid> UpdateUser(Guid id, string email, string password)
        {
            return await _userRepository.Update(id, email, password);
        }

        public async Task<Guid> DeleteUser(Guid id)
        {
            return await _userRepository.Delete(id);
        }



        private Result<User> FindExsitingUser(string email)
        {
            User existingUser = (User)_userRepository.FindExisting(email);
            
            if (existingUser == null)
            {
                Console.WriteLine("user not found - create success");
                return Result.Failure<User>("User not found");
            }

            return Result.Success(existingUser);
        }
        private Result<string> FindUnexsitingUser(string email)
        {
            User existingUser = (User)_userRepository.FindExisting(email);

            if (existingUser == null)
            {
                return Result.Success("User not found - success");
            }

            return Result.Failure<string>("User already exists - failed");
        }
        //private bool FindExisting(string email)
        //{
        //    return _userRepository.FindExisting(email);
        //}
    }
}
