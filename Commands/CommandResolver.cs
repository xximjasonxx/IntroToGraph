using GraphDemo.Services;

namespace GraphDemo.Commands
{
    public class CommandResolver
    {
        private readonly IQuerySource _querySource;

        public CommandResolver(IQuerySource querySource)
        {
            _querySource = querySource;
        }

        public ICommand CreateCommand(ApplicationOption option)
        {
            switch (option)
            {
                case ApplicationOption.CreateSong:
                    return new CreateSongCommand(_querySource);
            }

            throw new InvalidOperationException("The option provided does not have a command");
        }
    }
}