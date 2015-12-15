using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfGUI.ViewModels;

namespace WpfGUI
{
    public class ValidatePlayerCommand : ICommand
    {
        private PlayerViewModel playerViewModel;

        public ValidatePlayerCommand(PlayerViewModel playerViewModel)
        {
            this.playerViewModel = playerViewModel;
        }


        public bool CanExecute(object parameter)
        {
            return this.playerViewModel.ValidatePlayer();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            this.playerViewModel.SubmitPlayer();
        }
    }
}
