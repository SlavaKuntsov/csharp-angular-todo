using CSharpFunctionalExtensions;
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

        private Item(Guid id, Guid groupId, string title, string description, TodoStatus status)
        {
            Id = id;
            GroupId = groupId;
            Title = title;
            Description = description;
            Status = status;
        }
        private Item(Guid groupId, string title, string description, TodoStatus status)
        {
            GroupId = groupId;
            Title = title;
            Description = description;
            Status = status;
        }

        public static Result<Item> Create(Guid id, Guid groupId, string title, string description, TodoStatus status)
        {
            if (string.IsNullOrEmpty(title))
            {
                return Result.Failure<Item>("Title can not be empty");
            }

            Item item = new Item(id, groupId, title, description, status);

            return Result.Success(item);
        }
        public static Result<Item> Create(Guid groupId, string title, string description, TodoStatus status)
        {
            if (string.IsNullOrEmpty(title))
            {
                return Result.Failure<Item>("Title can not be empty");
            }

            Item item = new Item(groupId, title, description, status);

            return Result.Success(item);
        }
    }
}