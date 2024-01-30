namespace UserStore.DataAccess.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Token { get; set; }
        public List<GroupEntity> Groups { get; set; } = new List<GroupEntity>();
    }
}
