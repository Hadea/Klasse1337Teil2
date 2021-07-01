using System.Windows.Input;

namespace MemoryUI
{
    static class Commands
    {
        public static readonly RoutedUICommand Help = new(
            "Shows the help window",
            "Help",
            typeof(Commands),
            new InputGestureCollection() { new KeyGesture(Key.F1) });
    }
}
