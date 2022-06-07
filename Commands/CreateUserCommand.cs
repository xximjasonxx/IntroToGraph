using System;
using GraphDemo.Entities;
using GraphDemo.Property;
using GraphDemo.Services;

namespace GraphDemo.Commands
{
	public class CreateUserCommand : ICommand
	{
        private readonly IQuerySource _querySource;

        public CreateUserCommand(IQuerySource querySource)
        {
            _querySource = querySource;
        }

        public async Task ExecuteAsync()
        {
            var user = new User();
            var propertyWriter = new PropertyWriter<User>();

            // set properties
            propertyWriter.SetProperty("Enter First Name: ", value => user.FirstName = value);
            propertyWriter.SetProperty("Enter Last Name: ", value => user.LastName = value);

            // save the value
            var createdUser = await _querySource.AddVertex(user);
            Console.WriteLine("User Added");
        }
    }
}

