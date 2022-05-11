namespace GraphDemo.Menu
{
    public class MenuOption<TReturn>
    {
        public int OptionNumber { get; init; }
        public string OptionText { get; init; }
        public TReturn ReturnValue { get; init; }
    }
}