using System;
using System.Windows.Input;

namespace ZzaDashboard
{
    public class RelayCommand<T> : ICommand

    {
        private Action<T> _action;
        private Func<T, bool> _predicate;

        public RelayCommand(Action<T> action)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public RelayCommand(Action<T> action, Func<T, bool> predicate) : this(action)
        {
            _predicate = predicate;
        }

        public bool CanExecute(object parameter)
        {
            return _predicate?.Invoke((T) parameter) ?? true;
        }

        public void Execute(object parameter)
        {
            _action.Invoke((T) parameter);
        }

        public event EventHandler CanExecuteChanged;
    }
}