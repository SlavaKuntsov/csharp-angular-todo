using UserStore.Core.Abstractions;

namespace UserStore.DataAccess.Entities
{
    public class ItemEntity
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public TodoStatus Status { get; set; }
        public Guid GroupId { get; set; }
        public GroupEntity? Group { get; set; }
    }
    
}
