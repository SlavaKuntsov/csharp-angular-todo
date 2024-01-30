using CSharpFunctionalExtensions;
using UserStore.Core.Models;

namespace UserStore.Core.Abstractions
{
    public interface IGroupService
    {
        Task<List<Group>> GetAllGroups();
        Task<Result<Guid>> CreateGroup(Group group);
    }
}
