using GraphDemo.Services;

namespace GraphDemo.Commands
{
    public class CreateSongCommand : ICommand
    {
        private readonly IQuerySource _querySource;

        public CreateSongCommand(IQuerySource querySource)
        {
            _querySource = querySource;
        }

        public async Task ExecuteAsync()
        {
            Console.WriteLine("Creating Song");
        }
    }
}