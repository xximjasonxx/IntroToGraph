using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace GraphDemo.Entities
{
	public class User : Vertex, IEqualityComparer<User>
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }

        public override string Id => $"{FirstName}{LastName}";
        public override string ItemType => "User";

		public string Name => $"{FirstName} {LastName}";

        public bool Equals(User? x, User? y)
        {
            return x?.Id == y?.Id;
        }

        public int GetHashCode([DisallowNull] User obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}

