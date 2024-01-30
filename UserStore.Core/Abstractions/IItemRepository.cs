using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStore.Core.Models;

namespace UserStore.Core.Abstractions
{
    public interface IItemRepository
    {
        Task<List<Item>> Get();
        Task<Guid> Create(Item item);
    }
}
