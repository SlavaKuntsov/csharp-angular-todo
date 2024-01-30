using CSharpFunctionalExtensions;
using UserStore.Core.Abstractions;
using UserStore.Core.Models;

namespace UserStore.Application.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;

        public ItemService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<List<Item>> GetAllItems()
        {
            return await _itemRepository.Get();
        }
        public async Task<Result<Guid>> CreateItem(Item item)
        {
            return await _itemRepository.Create(item);
        }
    }
}
