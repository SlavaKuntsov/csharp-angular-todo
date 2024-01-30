using UserStore.Core.Abstractions;

namespace UserStore.API.Contracts
{
    public record ItemsResponse(
        Guid id,
        Guid groupUd,
        string title,
        string description,
        TodoStatus status
    );
}
