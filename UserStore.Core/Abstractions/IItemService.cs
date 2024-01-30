using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStore.Core.Models;

namespace UserStore.Core.Abstractions
{
    public interface IItemService
    {
        Task<List<Item>> GetAllItems();
        Task<Result<Guid>> CreateItem(Item item);
    }
}
