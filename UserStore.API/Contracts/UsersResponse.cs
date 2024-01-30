namespace UserStore.API.Contracts
{
    public record UsersResponse(
        Guid id,
        string email,
        string password,
        string token
    );
}
