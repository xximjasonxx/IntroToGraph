using System;
using GraphDemo.Services;

namespace GraphDemo.Commands
{
	public class DeleteAllCommand : ICommand
	{
        private readonly IQuerySource _querySource;

		public DeleteAllCommand(IQuerySource querySource)
		{
            _querySource = querySource;
		}

        public async Task ExecuteAsync()
        {
            Console.Write("Really delete the graph?: ");
            var answer = Console.ReadLine()?.ToLower();

            if (answer == "y")
            {
                await _querySource.DropAllVertices();
                Console.WriteLine("Data wipe complete");
            }
        }
    }
}

