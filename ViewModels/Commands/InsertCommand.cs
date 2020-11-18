using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CRUD_Application.ViewModels.Commands
{
    class InsertCommand : ICommand
    {
        PersonViewModel viewModel;

        public InsertCommand(PersonViewModel model)
        {
            this.viewModel = model;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            string[] result = GetAllFields(parameter);
            if (result == null)
                return false;
            if (result.Length != 4)
                return false;

            string forName = result[0];
            string surName = result[1];
            if (forName.Length < 3 || surName.Length < 3)
                return false;

            return true;
        }

        public void Execute(object parameter)
        {
            string[] result = GetAllFields(parameter);
            viewModel.ExecuteNonQueryCommand("INSERT INTO Person (FirstName, LastName, Gender, Age) " +
                "VALUES ('" + viewModel.Person.ForName + "', '" + viewModel.Person.SurName + 
                "', '" + viewModel.Person.Gender + "', '" + viewModel.Person.Age + "');");
        }

        public string[] GetAllFields(object paramater)
        {
            string input = (string)paramater;
            if (input == null)
                return null;

            char[] splitter = new char[] { ' ' };
            return input.Split(splitter);
        }
    }
}
