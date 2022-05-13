using GraphDemo.Commands;

namespace GraphDemo.MenuView
{
    public class MenuBuilder
    {
        private string? _promptText;
        private List<MenuOption> _options = new List<MenuOption>();

        public MenuBuilder AddPrompText(string promptText)
        {
            _promptText = promptText;
            return this;
        }

        public MenuBuilder AddOption(int optionNumber, string optionText, Func<ICommand> actionCommand)
        {
            _options.Add(new MenuOption
            {
                OptionNumber = optionNumber,
                OptionText = optionText,
                Command = actionCommand.Invoke()
            });

            return this;
        }

        public MenuBuilder AddExitOption(int optionNumber, string optionText)
        {
            _options.Add(new MenuOption
            {
                OptionNumber = optionNumber,
                OptionText = optionText,
                IsExit = true
            });

            return this;
        }

        public MenuView Build()
        {
            return new MenuView(
                promptText: string.IsNullOrEmpty(_promptText) ? "Please select an Option: " : _promptText,
                options: _options
            );
        }
    }
}