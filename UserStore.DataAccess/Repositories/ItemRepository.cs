using Microsoft.EntityFrameworkCore;
using UserStore.Core.Abstractions;
using UserStore.DataAccess.Entities;

namespace UserStore.DataAccess.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly UserStoreDbContext _context;
        public ItemRepository(UserStoreDbContext context)
        {
            _context = context;
        }
        public async Task<List<Core.Models.Item>> Get()
        {
            var itemEntities = await _context.Items
                .AsNoTracking()
                .ToListAsync();

            var items = itemEntities
                .Select(i => Core.Models.Item.Create(i.Id, i.GroupId, i.Title!, i.Description, i.Status).Value)
                .ToList();

            return items;
        }
        public async Task<Guid> Create(Core.Models.Item item)
        {
            Guid Id = Guid.NewGuid();
            var itemEntity = new ItemEntity
            {
                Id = Id,
                GroupId = item.GroupId,
                Title = item.Title,
                Description = item.Description,
                Status = item.Status
            };

            await _context.Items.AddAsync(itemEntity);
            await _context.SaveChangesAsync();

            return itemEntity.Id;
        }
    }
}
