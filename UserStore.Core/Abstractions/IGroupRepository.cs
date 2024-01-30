using UserStore.Core.Models;

namespace UserStore.Core.Abstractions
{
    public interface IGroupRepository
    {
        Task<List<Group>> Get();
        Task<Guid> Create(Group group);
    }
}
