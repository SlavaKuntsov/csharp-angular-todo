using CSharpFunctionalExtensions;

namespace UserStore.Core.Models
{
    public class Group
    {
        public Guid Id { get; }
        public string Title { get; }
        public Guid UserId { get; }
        public User? User { get; }
        public List<Item> Items { get; } = new List<Item>();

        private Group(Guid id, Guid userId, string title)
        {
            Id = id;
            UserId = userId;
            Title = title;
        }
        private Group(Guid userId, string title)
        {
            UserId = userId;
            Title = title;
        }

        public static Result<Group> Create(Guid id, Guid userId, string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                return Result.Failure<Group>("Title can not be empty");
            }

            Group group = new Group(id, userId, title);

            return Result.Success(group);
        }
        public static Result<Group> Create(Guid userId, string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                return Result.Failure<Group>("Title can not be empty");
            }

            Group group = new Group(userId, title);

            return Result.Success(group);
        }
    }
}
