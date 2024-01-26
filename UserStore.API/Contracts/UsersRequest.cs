namespace UserStore.API.Contracts
{
    public record UsersRequest(
        string email,
        string password);
    
    public record UsersRequestLogin(string token);

}
