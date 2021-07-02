using System;
using System.Windows.Input;

namespace ZzaDashboard
{
    public class RelayCommand:ICommand
    {
        private Action _action;
        private Func<bool> _predicate;

        public RelayCommand(Action action)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public RelayCommand(Action action, Func<bool> predicate) :this(action)
        {
            _predicate = predicate;
        }
        public bool CanExecute(object parameter)
        {
            return _predicate?.Invoke() ?? true;
        }

        public void Execute(object parameter)
        {
            _action.Invoke();
        }

        public event EventHandler CanExecuteChanged;

        public virtual void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}