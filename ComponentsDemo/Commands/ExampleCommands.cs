using System.Windows.Input;

namespace ComponentsDemo
{
    static class ExampleCommands
    {
        public static readonly RoutedUICommand Save = new RoutedUICommand("Save Command", "Save", typeof(ExampleCommands),
            new InputGestureCollection() {
                new KeyGesture(Key.S, ModifierKeys.Control),
                new KeyGesture(Key.E, ModifierKeys.Control) });
    }
}
