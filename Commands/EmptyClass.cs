using System;
using GraphDemo.Services;

namespace GraphDemo.Commands
{
	public class RecommendArtistCommand : ICommand
	{
		private readonly IQuerySource _querySource;

		public RecommendArtistCommand(IQuerySource querySource)
		{
		}

        public Task ExecuteAsync()
        {
            throw new NotImplementedException();
        }
    }
}

