using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using UserStore.Core.Abstractions;
using UserStore.Core.Models;
using UserStore.DataAccess.Entities;

namespace UserStore.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
	{
		private readonly UserStoreDbContext _context;
		public UserRepository(UserStoreDbContext context)
		{
			_context = context;
		}

		public async Task<List<User>> Get()
		{
            var userEntities = await _context.Users
				.AsNoTracking()
				.ToListAsync();

			var users = userEntities
				.Select(u => User.Create(u.Id, u.Email!, u.Password!, u.Token!).Value)
				.ToList();

			return users;
		}

		public async Task<string> Create(User user)
		{
            Guid Id = Guid.NewGuid();
            var userEntity = new UserEntity
            {
                Id = Id,
                Email = user.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(user.Password),
                Token = User.GenerateToken(Id, "9f6a1d7e5b3c8a4d9f6a1d7e5b3c8a4d")
            };
            
			await _context.Users.AddAsync(userEntity);
            await _context.SaveChangesAsync();

            return userEntity.Token;
        }

        #region old login
        //public Result<User> Login(User user)
        //{
        //	//	3. этот login должен ли включать в себя token
        //	// пока нет проверки по почте, будем считать что у сайта всегда есть токен


        //	//	2. можно ли избавиться от этой проверки?
        //	// проверка на: email, login, token

        //	//	1. зачем проверять еще и id из токена если дальше есть проверка на valid token

        //	string existingUserId = User.ExtractIdFromToken(user.Token!);

        //          if (existingUserId == null)
        //          {
        //		//return "User not found"; // or "Invalid token"
        //		return Result.Failure<User>("User not found");
        //          }

        //          // как просиходит auth
        //          // я присылаю токен при обнорвлении страницы, достается айди из него
        //          // но здесь же еще и проверяется valid password 

        //          // или разделить на два метода auth и Login 
        //          // auth при обновлении страницы
        //          // иначе login 

        //          // куда внедрить access token
        //          // является ли нынешний токен - refresh токеном

        //          // но как тогда проиходит первый login ведь там идет и email и password
        //          // 



        //          var existingUser = _context.Users.SingleOrDefault(u => u.Id == Guid.Parse(existingUserId));

        //          if (existingUser!.Email != user.Еmail)
        //          {
        //              //return "User not found";
        //              return Result.Failure<User>("User not found");
        //          }

        //          if (!BCrypt.Net.BCrypt.Verify(user.Password, existingUser!.Password))
        //	{
        //		//Console.WriteLine("wrong password");
        //              //return "Wrong password";
        //              return Result.Failure<User>("Wrong password");
        //          }

        //	if (!UserStore.Core.Models.User.ValidateToken(user.Token!, "9f6a1d7e5b3c8a4d9f6a1d7e5b3c8a4d"))
        //	{
        //		//Console.WriteLine("wrong token");
        //              //return "Invalid token";
        //              return Result.Failure<User>("Invalid token");
        //          }

        //	Console.WriteLine("done");

        //          //string guidString = existingUser.Id.ToString();

        //          //return guidString;
        //	return Result.Success(existingUser);
        //}
        #endregion

		public async Task<Guid> Update(Guid id, string email, string password)
		{
			await _context.Users
				.Where(u => u.Id == id)
				.ExecuteUpdateAsync(s => s
					.SetProperty(u => u.Email, u => email)
					.SetProperty(u => u.Password, u => password)
				);

			return id;
		}

		public async Task<Guid> Delete(Guid id)
		{
			await _context.Users
				.Where(u => u.Id == id)
				.ExecuteDeleteAsync();

			return id;
		}



        public Object FindExisting(string email)
        {
            return _context.Users!
                .SingleOrDefault(u => u.Email == email);
        }

    }
}
