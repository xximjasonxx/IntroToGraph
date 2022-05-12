using System;
namespace GraphDemo.Entities
{
	public class User : Vertex
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }

		public override string partitionKey => LastName.ToLower().Substring(0, 1);

		public override string label => FirstName + LastName;
    }
}

