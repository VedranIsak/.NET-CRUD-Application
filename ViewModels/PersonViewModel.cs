using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using CRUD_Application.Models;
using CRUD_Application.ViewModels.Commands;
using CRUD_Application.ViewModels.Converters;

/*
 * TO DO
 * Fixa att man kan "showa" individuella entries direkt på varandra
 * Fixa att man kan stänga applikationen?
 * Fixa felmeddelanden
 */

namespace CRUD_Application.ViewModels
{
    class PersonViewModel : INotifyPropertyChanged
    {
        PersonModel person;
        ObservableCollection<PersonModel> persons;
        GenderConverter genderConverter;
        SqlConnection connection;

        InsertCommand insertCommand;
        DeleteAllCommand deleteAllCommand;
        DeleteCommand deleteCommand;
        UpdateCommand updateCommand;
        SelectAllCommand selectAllCommand;
        SelectCommand selectCommand;

        public event PropertyChangedEventHandler PropertyChanged;

        public PersonViewModel()
        {
            Person = new PersonModel()
            {
                ForName = "",
                SurName = "",
                Gender = "",
                Age = 0,
                ID = 0
            };
     
            Persons = new ObservableCollection<PersonModel>();

            InsertCommand = new InsertCommand(this);
            DeleteAllCommand = new DeleteAllCommand(this);
            DeleteCommand = new DeleteCommand(this);
            UpdateCommand = new UpdateCommand(this);
            SelectAllCommand = new SelectAllCommand(this);
            SelectCommand = new SelectCommand(this);

            connection = new SqlConnection("Server = .; DataBase = PersonDatabase; Trusted_Connection = true;");
            connection.Open();

            Select("SELECT * FROM Person;");
        }

        public PersonModel Person
        {
            get { return person; }
            set { person = value; }
        }

        public ObservableCollection<PersonModel> Persons
        {
            get { return persons; }
            set { persons = value; }
        }

        public GenderConverter GenderConverter
        {
            get { return genderConverter; }
            set { genderConverter = value; }
        }

        public InsertCommand InsertCommand
        {
            get { return insertCommand; }
            set { insertCommand = value; }
        }

        public DeleteAllCommand DeleteAllCommand
        {
            get { return deleteAllCommand; }
            set { deleteAllCommand = value; }
        }

        public DeleteCommand DeleteCommand
        {
            get { return deleteCommand; }
            set { deleteCommand = value; }
        }

        public UpdateCommand UpdateCommand
        {
            get { return updateCommand; }
            set { updateCommand = value; }
        }
        
        public SelectAllCommand SelectAllCommand
        {
            get { return selectAllCommand; }
            set { selectAllCommand = value; }
        }

        public SelectCommand SelectCommand
        {
            get { return selectCommand; }
            set { selectCommand = value; }
        }

        public string ForName
        {
            get { return Person.ForName; }
            set
            {
                Person.ForName = value;
                OnPropertyChanged(nameof(ForName));
                OnPropertyChanged(nameof(FullInfo));
            }
        }

        public string SurName
        {
            get { return Person.SurName; }
            set
            {
                Person.SurName = value;
                OnPropertyChanged(nameof(SurName));
                OnPropertyChanged(nameof(FullInfo));
            }
        }

        public string Gender
        {
            get { return Person.Gender; }
            set
            {
                Person.Gender = value;
                OnPropertyChanged(nameof(Gender));
                OnPropertyChanged(nameof(FullInfo));
            }
        }

        public int Age
        {
            get { return Person.Age; }
            set
            {
                Person.Age = value;
                OnPropertyChanged(nameof(Age));
                OnPropertyChanged(nameof(FullInfo));
            }
        }

        public int ID
        {
            get { return Person.ID; }
            set
            {
                Person.ID = value;
                OnPropertyChanged(nameof(ID));
            }
        }

        public string FullInfo
        {
           get { return ForName + " " + SurName + " " + Gender + " " + Age.ToString(); }
        }

        void OnPropertyChanged(string methodName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(methodName));
        }

        public void Select(string command)
        {
            var selectCom = new SqlCommand(command, connection);
            var reader = selectCom.ExecuteReader();
            Persons.Clear();

            while (reader.Read())
            {
                Persons.Add(new PersonModel()
                {
                    ForName = reader.GetString(1),
                    SurName = reader.GetString(2),
                    Gender = reader.GetString(3),
                    Age = reader.GetInt32(4),
                    ID = reader.GetInt32(0)
                });
            }
            reader.Close();

        }

        public void ExecuteNonQueryCommand(string command)
        {
            var cmd = new SqlCommand(command, connection);
            cmd.ExecuteNonQuery();
            Select("SELECT * FROM Person;");
        }

    }
}
