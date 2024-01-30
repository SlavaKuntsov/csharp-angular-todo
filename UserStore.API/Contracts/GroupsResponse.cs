namespace UserStore.API.Contracts
{
    public record GroupsResponse(
        Guid id,
        Guid userId,
        string title
    );
}
