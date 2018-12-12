using System;
using System.Windows.Input;

namespace Mvvm
{
    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Func<T, bool> _canExecute;

        public RelayCommand(Action<T> execute, Func<T,bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException("execute");
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
                return true;

            if(parameter == null && typeof(T).IsValueType)
            {
                return _canExecute.Invoke(default(T));
            }

            if(parameter == null || parameter is T)
            {
                return _canExecute.Invoke((T)parameter);
            }

            return false;
        }

        public void Execute(object parameter)
        {
            var par = parameter;

            if(parameter.GetType() != typeof(T))
            {
                if(parameter is IConvertible)
                {
                    par = Convert.ChangeType(parameter, typeof(T), null);
                }
            }

            if(CanExecute(par) && _execute != null)
            {
                if(par == null)
                {
                    if (typeof(T).IsValueType)
                    {
                        _execute.Invoke(default(T));
                    }
                    else
                    {
                        _execute.Invoke((T)par);
                    }
                }
                else
                {
                    _execute.Invoke((T)par);
                }
            }
        }
    }
}
