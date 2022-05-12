using System;
using GraphDemo.Services;

namespace GraphDemo.Commands
{
	public class LikeArtistCommand : ICommand
	{
        private readonly IQuerySource _querySource;

		public LikeArtistCommand(IQuerySource querySource)
		{
		}

        public Task ExecuteAsync()
        {
            throw new NotImplementedException();
        }
    }
}

