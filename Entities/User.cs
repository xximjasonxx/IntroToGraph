using System;
using System.Text.Json.Serialization;

namespace GraphDemo.Entities
{
	public class User : Vertex
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }

		public override string partitionKey => LastName.ToLower().Substring(0, 1);

		[JsonIgnore]
		public string Name => $"{FirstName} {LastName}";
    }
}

