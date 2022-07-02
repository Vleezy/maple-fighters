using System;
using Authenticator.Domain.Repository;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Authenticator.Infrastructure
{
    public class Account : IEntity<string>
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.UtcNow;

        public Account(
            string email,
            string password,
            string firstName,
            string lastName)
        {
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Password = password ?? throw new ArgumentNullException(nameof(password));
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
        }

        public static Account Create(
            string email,
            string password,
            string firstName,
            string lastName)
        {
            return new Account(email, password, firstName, lastName);
        }
    }
}