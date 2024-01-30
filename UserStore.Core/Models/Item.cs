using UserStore.Core.Abstractions;

namespace UserStore.Core.Models
{
    public class Item
    {
        public Guid Id { get; }
        public string? Title { get; }
        public string? Description { get; }
        public TodoStatus Status { get; }
        public Guid GroupId { get; }
        public Group? Group { get; }
    }
}