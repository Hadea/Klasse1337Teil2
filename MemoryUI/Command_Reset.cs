using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace MemoryUI
{
    internal class Command_Reset : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        private readonly MainWindow mParentWindow;
        public Command_Reset(MainWindow parent)
        {
            mParentWindow = parent;
        }

        public bool CanExecute(object parameter)
        {
            return mParentWindow.VerticalSize > 0 && mParentWindow.HorizontalSize > 0;
        }

        public void Execute(object parameter)
        {
            //string[] folderList = new string[lvImageSets.SelectedItems.Count];
            //for (int counter = 0; counter < folderList.Length; counter++)
            //    folderList[counter] = lvImageSets.SelectedItems[counter].ToString();

            //mParentWindow.resetGame(mParentWindow.HorizontalSize, mParentWindow.VerticalSize, folderList); // startet den Reset-Vorgang anhand der mitgelieferten grösse.
        }
    }
}