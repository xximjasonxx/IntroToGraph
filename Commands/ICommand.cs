namespace GraphDemo.Commands
{
    public interface ICommand
    {
        Task ExecuteAsync();
    }
}