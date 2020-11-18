using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CRUD_Application.ViewModels.Commands
{
    class UpdateCommand : ICommand
    {
        PersonViewModel viewModel;
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public UpdateCommand(PersonViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            foreach (var Person in viewModel.Persons)
                if (Person.ID == viewModel.Person.ID)
                    return true;
            return false;
        }

        public void Execute(object parameter)
        {
            viewModel.ExecuteNonQueryCommand("UPDATE Person SET FirstName = '" + viewModel.Person.ForName + "'" +
                ", LastName = '" + viewModel.Person.SurName + "'" +
                ", Gender = '" + viewModel.Person.Gender + "'" +
                ", Age = '" + viewModel.Person.Age + "'" +
                "WHERE ID = '" + viewModel.Person.ID + "';");
        }
    }
}
