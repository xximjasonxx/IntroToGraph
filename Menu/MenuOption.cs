using GraphDemo.Commands;

namespace GraphDemo.MenuView
{
    public class MenuOption
    {
        public int OptionNumber { get; init; }
        public string OptionText { get; init; }

        public ICommand Command { get; init; }

        public bool IsExit { get; init; }
    }
}