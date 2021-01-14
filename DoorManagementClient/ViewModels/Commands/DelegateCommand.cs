using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DoorManagementClient.ViewModels
{
    public class DelegateCommand : ICommand
    {
        readonly Func<bool> _canExecute;
        readonly Action _execute;

        public DelegateCommand(Action execute) : this(execute, null) { }

        public DelegateCommand(Action execute, Func<bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }
        public void Execute(object parameter) { _execute(); }
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
