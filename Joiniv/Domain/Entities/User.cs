using System;

namespace Joiniv.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public required string Username { get; set;}
        public required string Email { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public  required string PasswordHash { get; set;}
        public UserRole Role { get; set; } = UserRole.Participant;
        public List<Event> Events { get; set; } = [];
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }

    public enum Gender
    {
        Male,
        Female,
        Other,
        PreferNotToSay
    }

    public enum UserRole
    {
        Admin,
        Organizer,
        Participant
    }
}