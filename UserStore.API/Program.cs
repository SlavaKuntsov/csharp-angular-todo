using Microsoft.EntityFrameworkCore;
using UserStore.Core.Abstractions;
using UserStore.DataAccess;
using UserStore.Application.Services;
using UserStore.DataAccess.Repositories;

namespace UserStore.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<UserStoreDbContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(UserStoreDbContext)));
            });

            builder.Services.AddScoped<IUsersService, UsersService>();
            builder.Services.AddScoped<IUserRepository, UserRepositiry>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors(builder =>
            {
                builder
                       .AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
                       //.WithOrigins("http://localhost:4200", "https://angular-todo-backend.onrender.com", "https://angular-todo-wine.vercel.app")
            });

            app.UseAuthorization();

            app.MapControllers();

            app.Run();

        }
    }
}