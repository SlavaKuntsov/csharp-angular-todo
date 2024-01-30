namespace UserStore.API.Contracts
{
    public record GroupsRequest(
        Guid userId,
        string title
    );
}
