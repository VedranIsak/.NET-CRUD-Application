using CRUD_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CRUD_Application.ViewModels.Commands
{
    class DeleteCommand : ICommand
    {
        PersonViewModel viewModel;
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public DeleteCommand(PersonViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            foreach(PersonModel person in viewModel.Persons)
            {
                if (viewModel.Person.ID == person.ID)
                    return true;
            }
            return false;
        }

        public void Execute(object parameter)
        {
            viewModel.ExecuteNonQueryCommand("DELETE FROM Person WHERE ID = '" + viewModel.Person.ID + "';");
        }
    }
}
