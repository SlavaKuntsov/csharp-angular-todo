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

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();     
            
            builder.Services.AddScoped<IGroupService, GroupService>();
            builder.Services.AddScoped<IGroupRepository, GroupRepository>();            
            
            builder.Services.AddScoped<IItemService, ItemService>();
            builder.Services.AddScoped<IItemRepository, ItemRepository>();

            var app = builder.Build();

            app.UseCors(builder =>
            {
                builder
                       .AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
                //.WithOrigins("http://localhost:4200", "https://angular-todo-backend.onrender.com", "https://angular-todo-wine.vercel.app")
            });

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();

        }
    }
}