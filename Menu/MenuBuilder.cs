namespace GraphDemo.Menu
{
    public class MenuBuilder<TReturn> where TReturn : System.Enum
    {
        private string? _promptText;
        private List<MenuOption<TReturn>> _options = new List<MenuOption<TReturn>>();

        public MenuBuilder<TReturn> AddPrompText(string promptText)
        {
            _promptText = promptText;
            return this;
        }

        public MenuBuilder<TReturn> AddOption(int optionNumber, string optionText, TReturn returnValue)
        {
            _options.Add(new MenuOption<TReturn>
            {
                OptionNumber = optionNumber,
                OptionText = optionText,
                ReturnValue = returnValue
            });

            return this;
        }

        public Menu<TReturn> Build()
        {
            return new Menu<TReturn>(
                promptText: string.IsNullOrEmpty(_promptText) ? "Please select an Option: " : _promptText,
                options: _options
            );
        }
    }
}