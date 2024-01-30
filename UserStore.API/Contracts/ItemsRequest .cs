using UserStore.Core.Abstractions;

namespace UserStore.API.Contracts
{
    public record ItemsRequest(
        Guid groupId,
        string title,
        string description,
        TodoStatus status
    );
}
