using System.Windows.Input;

namespace LibraryApp
{
    public class RelayCommand : ICommand
    {
        private readonly Action execute;
        private readonly Func<bool>? canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            ArgumentNullException.ThrowIfNull(execute);

            this.execute = execute;
            this.canExecute = canExecute;
        }
        public bool CanExecute(object? param)
        {
            return this.canExecute?.Invoke() != false;
        }
        public void Execute(object? param)
        {
            this.execute();
        }
    }
}
