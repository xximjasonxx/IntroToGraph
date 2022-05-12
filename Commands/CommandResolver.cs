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
                case ApplicationOption.CreateArtist:
                    return new CreateArtistCommand(_querySource);
                case ApplicationOption.CreateUser:
                    return new CreateUserCommand(_querySource);
                case ApplicationOption.LikeArtist:
                    return new LikeArtistCommand(_querySource);
                case ApplicationOption.RecommendArtist:
                    return new RecommendArtistCommand(_querySource);
            }

            throw new InvalidOperationException("The option provided does not have a command");
        }
    }
}