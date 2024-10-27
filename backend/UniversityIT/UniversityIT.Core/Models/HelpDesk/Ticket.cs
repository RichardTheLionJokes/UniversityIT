using CSharpFunctionalExtensions;

namespace UniversityIT.Core.Models.HelpDesk
{
    public class Ticket
    {
        private Ticket(Guid id,
            string name,
            string description,
            string place,
            DateTime createdAt,
            bool notificationsSent,
            bool isCompleted,
            Guid authorId,
            string? author)
        {
            Id = id;
            Name = name;
            Description = description;
            Place = place;
            CreatedAt = createdAt;
            NotificationsSent = notificationsSent;
            IsCompleted = isCompleted;
            AuthorId = authorId;
            Author = author;
        }

        public Guid Id { get; }
        public string Name { get; } = string.Empty;
        public string Description { get; } = string.Empty;
        public string Place { get; } = string.Empty;
        public DateTime CreatedAt { get; }
        public bool NotificationsSent { get; }
        public bool IsCompleted { get; }
        public Guid AuthorId { get; }
        public string? Author { get; } = string.Empty;

        public static Result<Ticket> Create(
            Guid id,
            string name,
            string description,
            string place,
            DateTime createdAt,
            bool notificationsSent,
            bool isCompleted,
            Guid authorId,
            string? author)
        {
            var ticket = new Ticket(id, name, description, place, createdAt, notificationsSent, isCompleted, authorId, author);

            return Result.Success(ticket);
        }
    }
}