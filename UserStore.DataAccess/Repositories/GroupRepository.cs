using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using UserStore.Core.Abstractions;
using UserStore.Core.Models;
using UserStore.DataAccess.Entities;

namespace UserStore.DataAccess.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly UserStoreDbContext _context;
        public GroupRepository(UserStoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<Core.Models.Group>> Get()
        {
            var groupEntities = await _context.Groups
                .AsNoTracking()
                .ToListAsync();

            var users = groupEntities
                .Select(g => Core.Models.Group.Create(g.Id, g.UserId, g.Title!).Value)
                .ToList();

            return users;
        }
        public async Task<Guid> Create(Core.Models.Group group)
        {
            Guid Id = Guid.NewGuid();
            var groupEntity = new GroupEntity
            {
                Id = Id,
                UserId = group.UserId,
                Title = group.Title
            };

            await _context.Groups.AddAsync(groupEntity);
            await _context.SaveChangesAsync();

            return groupEntity.Id;
        }
    }
}
