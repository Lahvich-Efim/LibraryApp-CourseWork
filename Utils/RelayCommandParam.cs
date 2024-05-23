using System.Windows.Input;

namespace LibraryApp
{
    public class RelayCommandParam : ICommand
    {
        private readonly Action<object> executePar;
        private readonly Func<object, bool> canExecutePar;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommandParam(Action<object> execute, Func<object, bool> canExecute = null)
        {
            ArgumentNullException.ThrowIfNull(execute);
            this.executePar = execute;
            this.canExecutePar = canExecute;
        }
        public bool CanExecute(object parameter)
        {
            return this.canExecutePar?.Invoke(parameter) != false;
        }

        public void Execute(object parameter)
        {
            this.executePar(parameter);
        }
    }
}
