using CSharpFunctionalExtensions;
using UserStore.Core.Abstractions;
using UserStore.Core.Models;

namespace UserStore.Application.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;

        public GroupService(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<List<Group>> GetAllGroups()
        {
            return await _groupRepository.Get();
        }

        public async Task<Result<Guid>> CreateGroup(Group group)
        {
            return await _groupRepository.Create(group);
        }
    }
}
