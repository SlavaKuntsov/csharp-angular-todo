using UserStore.Core.Models;

namespace UserStore.DataAccess.Entities
{
    public class GroupEntity
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public Guid UserId { get; set; }
        public UserEntity? User { get; set; }
        public List<ItemEntity> Items { get; set; } = new List<ItemEntity>();
    }
    
}
